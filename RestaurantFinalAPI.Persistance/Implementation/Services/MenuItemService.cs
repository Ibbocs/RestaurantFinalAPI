using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance;
using RestaurantFinalAPI.Application.DTOs.MenuItemDTOs;
using RestaurantFinalAPI.Application.DTOs.ExceptionDTOs;
using RestaurantFinalAPI.Application.DTOs.TableDTOs;
using RestaurantFinalAPI.Application.Enum;
using RestaurantFinalAPI.Application.Exceptions;
using RestaurantFinalAPI.Application.Exceptions.MenuItemCategoryExceptions;
using RestaurantFinalAPI.Application.Exceptions.MenuItemExceptions;
using RestaurantFinalAPI.Application.Exceptions.TableExceptions;
using RestaurantFinalAPI.Application.IRepositories.IMenuItemCategoryRepos;
using RestaurantFinalAPI.Application.IRepositories.IMenuItemRepos;
using RestaurantFinalAPI.Application.IRepositories.ITableRepos;
using RestaurantFinalAPI.Application.Models.ResponseModels;
using RestaurantFinalAPI.Application.UnitOfWork;
using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Persistance.Implementation.Services
{

    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemReadRepository menuItemRead;
        private readonly IMenuItemWriteRepository menuItemWrite;
        private readonly IUnitOfWork<MenuItem> unitOfWork;
        private readonly IMenuItemCategoryReadRepository menuItemCategoryRead;
        private readonly IMapper mapper;

        public MenuItemService(IMenuItemReadRepository _menuItemRead, IMenuItemWriteRepository _menuItemWrite, IUnitOfWork<MenuItem> _unitOfWork, IMapper _mapper, IMenuItemCategoryReadRepository _menuItemCategoryRead)
        {
            this.menuItemRead = _menuItemRead;
            this.menuItemWrite = _menuItemWrite;
            this.unitOfWork = _unitOfWork;
            this.mapper = _mapper;
            this.menuItemCategoryRead = _menuItemCategoryRead;
        }

        public async Task<Response<MenuItemCreateDTO>> AddMenuItem(MenuItemCreateDTO model)
        {
            if (model != null)
            {
                //catagory var yox onu yoxlayiram id gore //todo isdelete aciq yoxsa qapali olsun?
                var category = await menuItemCategoryRead.GetByIdAsync(model.MenuItemCategoryId, false);
                if (category == null)
                    throw new MenuItemCategoryGetFailedException(model.MenuItemCategoryId);

                /*bool addResult =*/
                await menuItemWrite.AddAsync(new()
                {
                    Description = model.Description,
                    Price = model.Price,
                    MenuItemName = model.MenuItemName,
                    MenuItemCategoryId = Guid.Parse(model.MenuItemCategoryId),

                });

                int result = await unitOfWork.SaveChangesAsync();

                if (result > 0 /*&& addResult*/)
                {
                    return new Response<MenuItemCreateDTO>()
                    {
                        Data = model,
                        StatusCode = 201
                    };
                }
                else
                {
                    //todo bu exception 2 yerde firlatdim, belke ozun yaradim??
                    throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 0), nameof(MenuItem)));
                }

            }
            else
            {
                return new Response<MenuItemCreateDTO>()
                {
                    Data = model,
                    StatusCode = 400
                };
            }

            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 0), nameof(MenuItem)));
        }

        public async Task<Response<bool>> DeleteMenuItem(string Id)
        {
            var data = await menuItemRead.GetByIdAsync(Id);

            if (data != null)
            {

                if (data.IsDeleted == false)
                {
                    await menuItemWrite.Remove(Id);
                    int result = await unitOfWork.SaveChangesAsync();

                    if (result == 1)
                    {
                        return new Response<bool>
                        {
                            Data = true,
                            StatusCode = 200
                        };
                    }
                    else
                    {
                        throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 2), nameof(MenuItem)));
                    }
                }
                else
                {
                    throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed("The specified resource has been deleted or is not available."));
                }

            }
            else
            {
                return new Response<bool>
                {
                    Data = false,
                    StatusCode = 404 //not found
                };
            }
            //todo bu kisima umumi hata yaratib versem?
            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 2), nameof(MenuItem)));
        }

        public async Task<Response<List<MenuItemGetDTO>>> GetAllMenuItem(bool isDelete = false)
        {
            var query = menuItemRead.GetAll(false);

            if (isDelete)
            {
                query = query.IgnoreQueryFilters();
            }

            query = query.Include(item => item.MenuItemCategory);
            var data = await query.ToListAsync();

            if (data != null && data.Count > 0)
            {
                var dtos = mapper.Map<List<MenuItemGetDTO>>(data);

                return new Response<List<MenuItemGetDTO>>
                {
                    Data = dtos,
                    StatusCode = 200
                };
            }
            else
            {
                return new Response<List<MenuItemGetDTO>>
                {
                    Data = null,
                    StatusCode = 404
                };
            }

            throw new MenuItemGetFaildException();
        }

        public async Task<Response<List<MenuItemGetDTO>>> GetAllMenuItemByCategory(string categoryId, bool isDeleted)
        {
            bool check = Guid.TryParse(categoryId, out var result);
            if (!check)
                throw new MenuItemCategoryGetFailedException(categoryId);
            //Eager loading
            var data = await menuItemRead.GetWithFiltir(i => i.MenuItemCategoryId == result, false, isDeleted).Include(item => item.MenuItemCategory).ToListAsync();

            if (data != null && data.Count > 0)
            {
                var dtos = mapper.Map<List<MenuItemGetDTO>>(data);

                return new Response<List<MenuItemGetDTO>>
                {
                    Data = dtos,
                    StatusCode = 200
                };
            }
            else
            {
                return new Response<List<MenuItemGetDTO>>
                {
                    Data = null,
                    StatusCode = 404
                };
            }

            throw new MenuItemGetFaildException();
        }

        public async Task<Response<MenuItemGetDTO>> GetMenuItemById(string Id, bool isDelete)
        {
            var query = menuItemRead.Table;
            MenuItem data = null;

            if (isDelete)
               data = await query.AsNoTracking().Include(m => m.MenuItemCategory).IgnoreQueryFilters().FirstOrDefaultAsync(m => m.Id == Guid.Parse(Id));
            else
                data = await query.AsNoTracking().Include(m => m.MenuItemCategory).FirstOrDefaultAsync(m => m.Id == Guid.Parse(Id));

            if (data != null)
            {
                var dtos = mapper.Map<MenuItemGetDTO>(data);

                return new Response<MenuItemGetDTO>
                {
                    Data = dtos,
                    StatusCode = 200
                };
            }
            else
            {
                return new Response<MenuItemGetDTO>
                {
                    Data = null,
                    StatusCode = 404
                };
            }
            throw new MenuItemGetFaildException(Id);
        }

        public async Task<Response<bool>> UpdateMenuItem(MenuItemUpdateDTO model)
        {
            var data = await menuItemRead.GetByIdAsync(model.MenuItemId);
            if (data != null)
            {
                //todo bUrda eslinde null gelecek datani deyismesin de elelemk lazimdiye,validation nezer salir amma description null gele biler.Duzdu validation bunu da qoymur amma adam onu update elemirse deyismeyeceyi bir sey elemeliyem mence
                data.Description = model.Description;
                data.Price = model.Price;
                data.MenuItemName = model.MenuItemName;
                data.MenuItemCategoryId = Guid.Parse(model.MenuItemCategoryId);


                menuItemWrite.Update(data);
                int result = await unitOfWork.SaveChangesAsync();

                if (result == 1)
                {
                    return new Response<bool>
                    {
                        Data = true,
                        StatusCode = 200
                    };
                }
                else
                {
                    throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 1), nameof(MenuItem)));
                }

            }
            else
                return new Response<bool> { Data = false, StatusCode = 404 };

            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 1), nameof(MenuItem)));
        }
    }
}
