using AutoMapper;
using BtgPactual.Back.Domain.Dtos;
using BtgPactual.Back.Domain.Dtos.Customers;
using BtgPactual.Back.Domain.Dtos.Customers.Response;
using BtgPactual.Back.Domain.Dtos.Funds;
using BtgPactual.Back.Domain.Models;
using BtgPactual.Back.Domain.Models.Customers;
using BtgPactual.Back.Domain.Models.Funds;
using BtgPactual.Back.Core.Helpers;
using MongoDB.Bson;

namespace BtgPactual.Back.Api.Mappings
{
    public class BtgPactualMappingProfile : Profile
    {
        public BtgPactualMappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();

            CreateMap<CustomerInfoResponse, CustomerDto>();
            CreateMap<CustomerDto, CustomerInfoResponse>();

            CreateMap<Fund, FundDto>();
            CreateMap<FundDto, Fund>();

            CreateMap<TransactionItem, FundTransactionDto>()
                .ForPath(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Parse(src.Date)));
            CreateMap<FundTransactionDto, TransactionItem>()
                .ForPath(dest => dest.Date, opt => opt.MapFrom(src => src.Date.FormatDateTime()));

            CreateMap<GeneralEntity, GeneralEntityDto>()
                .ForPath(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));
            CreateMap<GeneralEntityDto, GeneralEntity>()
                .ForPath(dest => dest.Id, opt => opt.MapFrom(src => new ObjectId(src.Id)));

            CreateMap<FundTransaction, FundTransactionDto>()
                .ForPath(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForPath(dest => dest.FundId, opt => opt.MapFrom(src => src.FundId.ToString()));
            CreateMap<FundTransactionDto, FundTransaction>()
                .ForPath(dest => dest.Id, opt => opt.MapFrom(src => new ObjectId(src.Id)))
                .ForPath(dest => dest.FundId, opt => opt.MapFrom(src => new ObjectId(src.FundId)));
        }
    }
}
