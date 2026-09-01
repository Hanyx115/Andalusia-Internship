using AutoMapper;
using Cinema.DTO;
using Cinema.Models;

namespace Cinema.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Movie
            CreateMap<Movie, MovieV1Dto>();
            CreateMap<Movie, MovieV2Dto>();
            CreateMap<CreateMovieRequest, Movie>();

            // Auditorium
            CreateMap<Auditorium, AuditoriumDto>();
            CreateMap<CreateAuditoriumRequest, Auditorium>();

            // ShowTime 
            CreateMap<ShowTime, ShowTimeDto>()
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.Auditorium.RoomNumber));
            CreateMap<CreateShowTimeRequest, ShowTime>();

            // Customer 
            CreateMap<Customer, CustomerDto>();
            CreateMap<CreateCustomerRequest, Customer>();

            // booking
            CreateMap<Booking, BookingDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.Customer.Email))
                .ForMember(dest => dest.ShowDateTime, opt => opt.MapFrom(src => src.ShowTime.ShowDateTime))
                .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.ShowTime.MovieId))
                .ForMember(dest => dest.MovieName, opt => opt.MapFrom(src => src.ShowTime.Movie.Name))
                .ForMember(dest => dest.AuditoriumId, opt => opt.MapFrom(src => src.ShowTime.AuditoriumId))
                .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.ShowTime.Auditorium.RoomNumber));
        }
    }
}
