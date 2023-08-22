using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance;
using RestaurantFinalAPI.Application.DTOs.ExceptionDTOs;
using RestaurantFinalAPI.Application.DTOs.MenuItemCategoryDTOs;
using RestaurantFinalAPI.Application.DTOs.TableDTOs;
using RestaurantFinalAPI.Application.Enum;
using RestaurantFinalAPI.Application.Exceptions;
using RestaurantFinalAPI.Application.Exceptions.MenuItemCategoryExceptions;
using RestaurantFinalAPI.Application.Exceptions.TableExceptions;
using RestaurantFinalAPI.Application.IRepositories.IMenuItemCategoryRepos;
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
    public class MenuItemCategoryService : IMenuItemCategoryService
    {
        private readonly IMenuItemCategoryReadRepository menuItemCategoryRead;
        private readonly IMenuItemCategoryWriteRepository menuItemCategoryWrite;
        private readonly IUnitOfWork<MenuItemCategory> unitOfWork;
        private readonly IMapper mapper;

        public MenuItemCategoryService(IUnitOfWork<MenuItemCategory> _unitOfWork, IMenuItemCategoryWriteRepository _menuItemCategoryWrite, IMenuItemCategoryReadRepository _menuItemCategoryRead, IMapper _mapper)
        {
            this.unitOfWork = _unitOfWork;
            this.menuItemCategoryWrite = _menuItemCategoryWrite;
            this.menuItemCategoryRead = _menuItemCategoryRead;
            this.mapper = _mapper;
        }

        public async Task<Response<MenuItemCategoryCreateDTO>> AddMenuItemCategory(MenuItemCategoryCreateDTO model)
        {
            if (model!=null)
            {
                await menuItemCategoryWrite.AddAsync(new()
                {
                    Description = model.Description,
                    CategoryName = model.CategoryName,
                });
                
                int result = await unitOfWork.SaveChangesAsync();

                if (result>0)
                {
                    return new Response<MenuItemCategoryCreateDTO>
                    {
                        Data = model,
                        StatusCode = 201,
                    };
                }
                else
                {
                    throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 0), nameof(MenuItemCategory)));
                }
            }
            else
            {
                return new Response<MenuItemCategoryCreateDTO>
                {
                    Data = model,
                    StatusCode = 400
                };
            }

            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 0), nameof(Table)));

        }

        public async Task<Response<bool>> DeleteMenuItemCategory(string Id)
        {
            var data = await menuItemCategoryRead.GetByIdAsync(Id);

            if (data != null)
            {
                if (data.IsDeleted == false)
                {
                    await menuItemCategoryWrite.Remove(Id);

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
                        throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 2), nameof(MenuItemCategory)));
                    }
                }
                else
                {
                    //todo burda softdelete false cekib silinmeden qurtara bilerem
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
            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 2), nameof(MenuItemCategory)));
        }

        public async Task<Response<bool>> UpdateMenuItemCategory(MenuItemCategoryUpdateDTO model)
        {
            var data = await menuItemCategoryRead.GetByIdAsync(model.MenuItemCategoryId);

            if (data != null)
            {
                data.Description = model.Description;
                data.CategoryName = model.CategoryName;

                menuItemCategoryWrite.Update(data);
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
                    throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 1), nameof(MenuItemCategory)));
                }

            }
            else
                return new Response<bool> { Data = false, StatusCode = 404 };

            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 1), nameof(MenuItemCategory)));
        }

        public async Task<Response<List<MenuItemCategoryGetDTO>>> GetAllMenuItemCategory(bool isDelete = false)
        {
            var query = menuItemCategoryRead.GetAll(false);

            if (isDelete)
            {
                query = query.IgnoreQueryFilters();
            }

            var data = await query.ToListAsync();

            if (data != null && data.Count>0)
            {
                var dtos = mapper.Map<List<MenuItemCategoryGetDTO>>(data);

                return new Response<List<MenuItemCategoryGetDTO>>
                {
                    Data = dtos,
                    StatusCode = 200
                };
            }
            else
            {
                return new Response<List<MenuItemCategoryGetDTO>>
                {
                    Data = null,
                    StatusCode = 404
                };
            }

            throw new MenuItemCategoryGetFailedException();
        }

        public async Task<Response<MenuItemCategoryGetDTO>> GetMenuItemCategoryById(string Id, bool isDelete)
        {
            var data = await menuItemCategoryRead.GetByIdAsync(Id, false, isDelete);

            if (data != null)
            {
                var dtos = mapper.Map<MenuItemCategoryGetDTO>(data);

                return new Response<MenuItemCategoryGetDTO>
                {
                    Data = dtos,
                    StatusCode = 200
                };
            }
            else
            {
                return new Response<MenuItemCategoryGetDTO>
                {
                    Data = null,
                    StatusCode = 404
                };
            }
            throw new MenuItemCategoryGetFailedException(Id);
        }
    }
}
