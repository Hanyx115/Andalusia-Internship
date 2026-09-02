using AutoMapper;
using TaskManagerApi.Models;
using TaskManagerApi.DTOs;
namespace TaskManagerApi.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TaskItem, TaskItemDto>();
        CreateMap<TaskItem, TaskSummaryDto>();
        CreateMap<CreateTaskRequest, TaskItem>()
            .ForMember(d => d.UserId, o => o.Ignore())
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Title, o => o.MapFrom(s => s.Title.Trim()))
            .ForMember(d => d.CreatedAt, o => o.MapFrom(s => DateTime.UtcNow))
            .ForMember(d => d.IsCompleted, o => o.MapFrom(s => false))
            .ForMember(d => d.UpdatedAt, o => o.Ignore());
        CreateMap<UpdateTaskRequest, TaskItem>()
            .ForMember(d => d.UserId, o => o.Ignore())
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Title, o => o.MapFrom(s => s.Title.Trim()))
            .ForMember(d => d.CreatedAt, o => o.Ignore())
            .ForMember(d => d.UpdatedAt, o => o.Ignore());
    }
}
