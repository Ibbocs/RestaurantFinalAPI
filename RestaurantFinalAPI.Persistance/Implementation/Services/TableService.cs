using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance;
using RestaurantFinalAPI.Application.DTOs.ExceptionDTOs;
using RestaurantFinalAPI.Application.DTOs.TableDTOs;
using RestaurantFinalAPI.Application.Enum;
using RestaurantFinalAPI.Application.Exceptions;
using RestaurantFinalAPI.Application.Exceptions.TableExceptions;
using RestaurantFinalAPI.Application.IRepositories.ITableRepos;
using RestaurantFinalAPI.Application.Models.ResponseModels;
using RestaurantFinalAPI.Application.UnitOfWork;
using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Table = RestaurantFinalAPI.Domain.Entities.RestaurantDBContext.Table;

namespace RestaurantFinalAPI.Persistance.Implementation.Services
{
    public class TableService : ITableService
    {
        private readonly ITableReadRepository tableRead;
        private readonly ITableWriteRepository tableWrite;
        private readonly IUnitOfWork<Table> unitOfWork;
        private readonly IMapper mapper;

        public TableService(ITableReadRepository _tableRead, ITableWriteRepository _tableWrite, IUnitOfWork<Table> _unitOfWork, IMapper _mapper )
        {
            this.tableRead = _tableRead;
            this.tableWrite = _tableWrite;
            this.unitOfWork = _unitOfWork;
            this.mapper = _mapper;
        }

        public async Task<Response<TableCreateDTO>> AddTable(TableCreateDTO? model)
        {
            if (model != null)
            {
                /*bool addResult =*/
                await tableWrite.AddAsync(new()
                {
                    TableName = model.TableName,
                    Capacity = model.Capacity,
                    Description = model.Description,
                });

                int result = await unitOfWork.SaveChangesAsync();

                if (result > 0 /*&& addResult*/)
                {
                    return new Response<TableCreateDTO>()
                    {
                        Data = model,
                        StatusCode = 201
                    };
                }
                else
                {
                    //todo bu exception 2 yerde firlatdim, belke ozun yaradim??
                    throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 0), nameof(Table)));
                }

            }
            else
            {
                return new Response<TableCreateDTO>()
                {
                    Data = model,
                    StatusCode = 400
                };
            }

            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 0), nameof(Table)));
        }

        public async Task<Response<bool>> ChangeOccupiedStatusForTable(string Id, bool IsOccupied)
        {

            var data = await tableRead.GetByIdAsync(Id);

            if (data != null)
            {
                if (data.IsOccupied != IsOccupied)
                {
                    data.IsOccupied = IsOccupied;
                    tableWrite.Update(data);

                    int result = await unitOfWork.SaveChangesAsync();

                    if (result == 1)
                    {
                        return new Response<bool>()
                        {
                            Data = true,
                            StatusCode = 200 //204 -data update oldu amma geri bodyde donulmedise, ancaq men respons donmeliyem body ile
                        };
                    }
                    else
                    {
                        throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.TableChangeOccupiedFailed());
                    }
                }
                else
                {
                    //todo 2 defe firlatdim burda uje occiped eyni gelse ona ozel xeta vere bilerem,yuxaridad da
                    throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.TableChangeOccupiedFailed("Table occupancy status is already set to the same value."));
                }
                
            }
            else
            {
                return new Response<bool> { Data = false, StatusCode = 404 /*not found*/};
            }

            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.TableChangeOccupiedFailed());
        }

        public async Task<Response<bool>> DeleteTable(string Id)
        {
            var data = await tableRead.GetByIdAsync(Id);

            if (data != null)
            {
                
                if (data.IsDeleted == false)
                {
                    await tableWrite.Remove(Id);
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
                        throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 2), nameof(Table)));
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
            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 2), nameof(Table)));
        }

        public async Task<Response<List<TableGetDTO>>> GetAllTable(bool isDelete)
        {
            //var data = await tableRead.GetAll(false).IgnoreQueryFilters().ToListAsync();

            var query = tableRead.GetAll(false);

            if (isDelete)
            {
                query =  query.IgnoreQueryFilters();
            }

            var data = await query.ToListAsync();

            if (data != null && data.Count>0)
            {
                var dtos = mapper.Map<List<TableGetDTO>>(data);

                return new Response<List<TableGetDTO>>
                {
                    Data = dtos,
                    StatusCode = 200
                };
            }
            else
            {
                return new Response<List<TableGetDTO>>
                {
                    Data = null,
                    StatusCode = 404
                };
            }

            throw new TableGetFailedException();
        }

        public async Task<Response<TableGetDTO>> GetTableById(string Id, bool isDelete)
        {

            var data = await tableRead.GetByIdAsync(Id, false, isDelete);

            if (data != null)
            {
                var dtos = mapper.Map<TableGetDTO>(data);

                return new Response<TableGetDTO>
                {
                    Data = dtos,
                    StatusCode = 200
                };
            }
            else
            {
                return new Response<TableGetDTO>
                {
                    Data = null,
                    StatusCode = 404
                };
            }
            throw new TableGetFailedException(Id);
        }

        public async Task<Response<bool>> UpdateTable(TableUpdateDTO model)
        {
            var data = await tableRead.GetByIdAsync(model.TableId);
            if (data != null)
            {
                data.TableName = model.TableName;
                data.Capacity = model.Capacity;
                data.Description = model.Description;

                tableWrite.Update(data);
                int result = await unitOfWork.SaveChangesAsync();

                if (result==1)
                {
                    return new Response<bool>
                    {
                        Data = true,
                        StatusCode = 200
                    };
                }
                else
                {
                    throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 1), nameof(Table)));
                }
                    
            }
            else
                return new Response<bool> { Data = false, StatusCode= 404 };

            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 1), nameof(Table)));
        }
    }
}
