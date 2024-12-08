
using AutoMapper;
using Business.DTOs;
using Domain.Entities.Card;

namespace Business.Mappers
{
    public class CardMapper : Profile
    {
        public CardMapper()
        {
            // Entity to DTO mapping
            CreateMap<Card, CardDto>();
            CreateMap<CardQuestion, CardQuestionDto>();
            CreateMap<CardQuestionChoice, CardQuestionChoiceDto>();

            // DTO to Entity mapping
            CreateMap<CardDto, Card>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CardQuestionDto, CardQuestion>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); 

            CreateMap<CardQuestionChoiceDto, CardQuestionChoice>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            // DTO to Entity mapping
            CreateMap<CardUpdateDto, Card>();

            CreateMap<CardQuestionUpdateDto, CardQuestion>();

            CreateMap<CardQuestionChoiceUpdateDto, CardQuestionChoice>();

        }
    }

}
