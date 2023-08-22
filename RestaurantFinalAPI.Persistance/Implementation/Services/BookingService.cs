using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantFinalAPI.Application.Abstraction.Servıces.Persistance;
using RestaurantFinalAPI.Application.DTOs.BookingDTOs;
using RestaurantFinalAPI.Application.DTOs.ExceptionDTOs;
using RestaurantFinalAPI.Application.DTOs.TableDTOs;
using RestaurantFinalAPI.Application.Enum;
using RestaurantFinalAPI.Application.Exceptions;
using RestaurantFinalAPI.Application.Exceptions.BookingExceptions;
using RestaurantFinalAPI.Application.Exceptions.CommonExceptions;
using RestaurantFinalAPI.Application.Exceptions.TableExceptions;
using RestaurantFinalAPI.Application.Exceptions.UserExceptions;
using RestaurantFinalAPI.Application.IRepositories.IBookingRepos;
using RestaurantFinalAPI.Application.IRepositories.ITableRepos;
using RestaurantFinalAPI.Application.Models.ResponseModels;
using RestaurantFinalAPI.Application.UnitOfWork;
using RestaurantFinalAPI.Domain.Entities.Identity;
using RestaurantFinalAPI.Domain.Entities.RestaurantDBContext;
using RestaurantFinalAPI.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantFinalAPI.Persistance.Implementation.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingReadRepository bookingRead;
        private readonly IBookingWriteRepository bookingWrite;
        private readonly IUnitOfWork<Booking> unitOfWork;
        private readonly IMapper mapper;
        private readonly ITableReadRepository tableRead;
        private readonly ITableWriteRepository tableWrite;
        readonly UserManager<AppUser> userManager;
        private readonly RestaurantFinalAPIContext context;


        public BookingService(IBookingReadRepository _bookingRead, IBookingWriteRepository _bookingWrite, IUnitOfWork<Booking> _unitOfWork, IMapper _mapper, UserManager<AppUser> _userManager, ITableReadRepository _tableRead, ITableWriteRepository tableWrite, RestaurantFinalAPIContext _context)
        {
            this.bookingRead = _bookingRead;
            this.bookingWrite = _bookingWrite;
            this.unitOfWork = _unitOfWork;
            this.mapper = _mapper;
            this.userManager = _userManager;
            this.tableRead = _tableRead;
            this.tableWrite = tableWrite;
            this.context = _context;
        }


        //bookingi qebul elemek ucun boking id uzerinden
        public async Task<Response<bool>> AcceptReservasion(string Id)//isconfrimd true, table reserved true
        {
            if (!Guid.TryParse(Id, out Guid newId))
                throw new InvalidIdFormatException(Id);

            var booking = await bookingRead.Table.Include(b => b.Table).FirstOrDefaultAsync(b => b.Id == newId);

            if (booking == null)
                return new Response<bool> { Data = false, StatusCode = 404 };

            if (booking.IsConfrimed)
                return new Response<bool> { Data = false, StatusCode = 400 };

            var table = booking.Table;

            if (table.IsReserved)
                throw new IsReservedException();

            using (var transaction = await context.Database.BeginTransactionAsync(IsolationLevel.RepeatableRead))
            {
                try
                {
                    booking.IsConfrimed = true;
                    bookingWrite.Update(booking);

                    table.IsReserved = true;
                    tableWrite.Update(table);

                    await unitOfWork.SaveChangesAsync();

                    transaction.Commit();

                    return new Response<bool> { Data = true, StatusCode = 200 };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed("Changing confirmed status or reservation status of table failed. Please try again later"));
                }
            }

            #region transaction olmayan
            //if (!(Guid.TryParse(Id, out Guid newId)))
            //    throw new InvalidIdFormatException(Id);

            ////var data = await bookingRead.GetByIdAsync(Id);
            //var data = await bookingRead.Table.Include(b => b.Table).FirstOrDefaultAsync(b=>b.Id== newId);

            ////data2.Table.IsReserved
            //if (data != null)
            //{
            //    //confirimd olmayiibsa onu true cekirem
            //    if (data.IsConfrimed == false)
            //    {
            //        //table bookingi var yox?
            //        //var table = await tableRead.GetByIdAsync((data.TableId).ToString()); //todo ola bilsin burda connectionlar qarisin respons donen yerde yaz amma uje confrimd deyisib deye onu da reject methodu ile sil
            //        var table = data.Table;

            //        if (table.IsReserved == true)
            //            throw new IsReservedException();

            //        data.IsConfrimed = true;

            //        bookingWrite.Update(data);
            //        int result = await unitOfWork.SaveChangesAsync();

            //        if (result == 1)
            //        {
            //            //tablenin de booking silirem

            //            table.IsReserved = true;
            //            tableWrite.Update(table);
            //            await unitOfWork.SaveChangesAsync(); //todo bu unit of work baglamaya biler cunki bookingindi

            //            return new Response<bool>
            //            {
            //                Data = true,
            //                StatusCode = 200
            //            };
            //        }
            //        else
            //        {
            //            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed("Changing confirmed status or reservation status of table failed. Please try again late"));
            //        }

            //    }
            //    else
            //    {
            //        return new Response<bool> { Data = false, StatusCode= 400 };
            //    }
            //}
            //else
            //{
            //    return new Response<bool> { Data = false, StatusCode = 404 };
            //}

            //throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed("Changing confirmed status or reservation status of table failed. Please try again late"));
            #endregion
        }

        public async Task<Response<BookingCreateDTO>> AddBooking(BookingCreateDTO model)
        {
            //user var
            var user = await userManager.FindByIdAsync(model.UserId);
            if (user == null)
                throw new NotFoundUserException();

            //id yoxlayriam formatin
            bool checkIdFormat = Guid.TryParse(model.TableId, out Guid guid);
            if (!checkIdFormat)
                throw new InvalidIdFormatException(model.TableId);

            //table isReserved bosmu, table var yoxla
            var table = await tableRead.GetByIdAsync(model.TableId);
            if (table == null || table?.IsReserved == true) //todo eslinde tarixe gore reservasiyon elemelisen mueyyen araliq ucunn rezervasiyon edile bilinse olar bu
                throw new TableGetFailedException(new ExceptionDTO
                {
                    Message = "Table Is Reserved Or Not Found!"
                });


            if (model != null)
            {
                /*bool addResult =*/
                await bookingWrite.AddAsync(new()
                {
                    StartDate = model.StartDate,
                    UserId = model.UserId,
                    TableId = guid,
                });

                int result = await unitOfWork.SaveChangesAsync();

                if (result > 0 /*&& addResult*/)
                {
                    return new Response<BookingCreateDTO>()
                    {
                        Data = model,
                        StatusCode = 201
                    };
                }
                else
                {
                    //todo bu exception 2 yerde firlatdim, belke ozun yaradim??
                    throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 0), nameof(Booking)));
                }

            }
            else
            {
                return new Response<BookingCreateDTO>()
                {
                    Data = model,
                    StatusCode = 400
                };
            }

            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 0), nameof(Booking)));
        }

        public async Task<Response<bool>> DeleteBooking(string Id)
        {
            var data = await bookingRead.GetByIdAsync(Id);

            if (data != null)
            {

                if (data.IsConfrimed == false)//isdeletet funksiyasi olsa burda yoxlanmalidi.
                {
                    await bookingWrite.Remove(Id);
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
                        throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 2), nameof(Booking)));
                    }
                }
                else
                {
                    throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed("The specified resource has been deleted or is not available for deletion because it is booking confirmed."));
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
            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 2), nameof(Booking)));
        }

        public async Task<Response<List<BookingGetDTO>>> GetAllBooking()
        {
            var query = bookingRead.GetAll(false);

            var data = await query.ToListAsync();

            if (data != null && data.Count > 0)
            {
                var dtos = mapper.Map<List<BookingGetDTO>>(data);

                return new Response<List<BookingGetDTO>>
                {
                    Data = dtos,
                    StatusCode = 200
                };
            }
            else
            {
                return new Response<List<BookingGetDTO>>
                {
                    Data = null,
                    StatusCode = 404
                };
            }

            throw new BookingGetFailedException();
        }

        public async Task<Response<List<BookingGetDTO>>> GetAllBookingByUserId(string Id)
        {
            //id uygun user var yox?
            var user = await userManager.FindByIdAsync(Id);
            if (user == null)
                throw new NotFoundUserException();

            var data = await bookingRead.GetWithFiltir(b => b.UserId == Id).ToListAsync();

            if (data != null && data.Count > 0)
            {
                var dtos = mapper.Map<List<BookingGetDTO>>(data);

                return new Response<List<BookingGetDTO>>
                {
                    Data = dtos,
                    StatusCode = 200
                };
            }
            else
            {
                return new Response<List<BookingGetDTO>>
                {
                    Data = null,
                    StatusCode = 404
                };
            }

            throw new BookingGetFailedException();
        }

        public async Task<Response<BookingGetDTO>> GetBookingById(string Id)
        {
            var data = await bookingRead.GetByIdAsync(Id, false);

            if (data != null)
            {
                var dtos = mapper.Map<BookingGetDTO>(data);

                return new Response<BookingGetDTO>
                {
                    Data = dtos,
                    StatusCode = 200
                };
            }
            else
            {
                return new Response<BookingGetDTO>
                {
                    Data = null,
                    StatusCode = 404
                };
            }
            throw new BookingGetFailedException(Id);
        }

        public async Task<Response<bool>> RejectReservasion(string Id)
        {
            if (!Guid.TryParse(Id, out Guid newId))
                throw new InvalidIdFormatException(Id);

            var booking = await bookingRead.Table.Include(b => b.Table).FirstOrDefaultAsync(b => b.Id == newId);

            if (booking == null)
                return new Response<bool> { Data = false, StatusCode = 404 };

            //confrimed false olarsa artiq silirem bu bookingi admin terefi ile
            if (booking.IsConfrimed == false)
                return await DeleteBooking(Id);

            var table = booking.Table;

            //if (table.IsReserved)
            //    throw new IsReservedException();

            using (var transaction = await context.Database.BeginTransactionAsync(IsolationLevel.RepeatableRead))
            {
                try
                {
                    booking.IsConfrimed = false;
                    bookingWrite.Update(booking);

                    table.IsReserved = false;
                    tableWrite.Update(table);

                    await unitOfWork.SaveChangesAsync();

                    transaction.Commit();

                    return new Response<bool> { Data = true, StatusCode = 200 };
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed("Changing confirmed status or reservation status of table failed. Please try again later"));
                }
            }

            //var data = await bookingRead.GetByIdAsync(Id);

            //if (data != null)
            //{
            //    //confirimd olubsa onu false cekirem
            //    if (data.IsConfrimed == true)
            //    {

            //        data.IsConfrimed = false;

            //        bookingWrite.Update(data);
            //        int result = await unitOfWork.SaveChangesAsync();

            //        if (result == 1)
            //        {
            //            //tablenin de booking silirem
            //            var table = await tableRead.GetByIdAsync((data.TableId).ToString());
            //            table.IsReserved = false;
            //            tableWrite.Update(table);
            //            await unitOfWork.SaveChangesAsync();

            //            return new Response<bool>
            //            {
            //                Data = true,
            //                StatusCode = 200
            //            };
            //        }
            //        else
            //        {
            //            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed("Changing confirmed status failed. Please try again late"));
            //        }

            //    }
            //    else
            //    {
            //        //confirmed falsedise silirem bookingi
            //        return await DeleteBooking(Id);
            //    }
            //}

            //throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed("Changing confirmed status or Deleting booking failed. Please try again late"));
        }

        public async Task<Response<bool>> UpdateBooking(BookingUpdateDTO model)
        {
            var data = await bookingRead.GetByIdAsync(model.BookingId);
            if (data != null)
            {
                //confirmed olub olmadigin yoxla
                if (!(data.IsConfrimed == false))
                    throw new IsConfirmedException();

                data.StartDate = model.StartDate;
                data.TableId = Guid.Parse(model.TableId);

                bookingWrite.Update(data);
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
                    throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 1), nameof(Booking)));
                }

            }
            else
                return new Response<bool> { Data = false, StatusCode = 404 };

            throw new GenericCustomException<ExceptionDTO>(CustomExceptionMessages.AnyEntityOperationFailed(Enum.GetName(typeof(ActionType), 1), nameof(Booking)));
        }
    }
}
