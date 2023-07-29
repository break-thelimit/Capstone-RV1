using AutoMapper;
using BusinessObjects.Model;
using DataAccess.Dtos.AnswerDto;
using DataAccess.Dtos.EventDto;
using DataAccess.Dtos.EventTaskDto;
using DataAccess.Dtos.ExchangeHistoryDto;
using DataAccess.Dtos.GiftDto;
using DataAccess.Dtos.InventoryDto;
using DataAccess.Dtos.ItemDto;
using DataAccess.Dtos.ItemInventoryDto;
using DataAccess.Dtos.LocationDto;
using DataAccess.Dtos.MajorDto;
using DataAccess.Dtos.NPCDto;
using DataAccess.Dtos.PlayerDto;
using DataAccess.Dtos.PlayerHistoryDto;
using DataAccess.Dtos.QuestionDto;
using DataAccess.Dtos.RankDto;
using DataAccess.Dtos.RoleDto;
using DataAccess.Dtos.SchoolDto;
using DataAccess.Dtos.SchoolEventDto;
using DataAccess.Dtos.TaskDto;
using DataAccess.Dtos.UserDto;
using DataAccess.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DataAccess.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            #region Event
            CreateMap<Event, GetEventDto>().ReverseMap();
            CreateMap<Event, UpdateEventDto>().ReverseMap();
            CreateMap<Event, CreateEventDto>().ReverseMap();
            CreateMap<Event, EventDto>().ReverseMap();
            #endregion


            #region Task
            CreateMap<Task, GetTaskDto>().ReverseMap();

            CreateMap<Task, UpdateTaskDto>().ReverseMap();

            CreateMap<Task, CreateTaskDto>().ReverseMap();
                      

            CreateMap<Task, TaskDto>()
                       .ForMember(dest => dest.MajorName, opt => opt.MapFrom(src => src.Major.Name))
                       .ForMember(dest => dest.NpcName, opt => opt.MapFrom(src => src.Npc.Name))
                       .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Location.LocationName))
                       .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Item.Name));
            CreateMap<TaskDto, Task>()
                        .ForMember(dest => dest.Major, opt => opt.Ignore())
                        .ForMember(dest => dest.Npc, opt => opt.Ignore())
                        .ForMember(dest => dest.Location, opt => opt.Ignore())
                        .ForMember(dest => dest.Item, opt => opt.Ignore());
            #endregion


            #region Event Task
            CreateMap<EventTask, GetEventTaskDto>().ReverseMap();

            CreateMap<EventTask, UpdateEventTaskDto>().ReverseMap();

            CreateMap<EventTask, CreateEventTaskDto>().ReverseMap();

            CreateMap<EventTask, EventTaskDto>()
                       .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Event.Name))
                       .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.Task.Name));
            #endregion

            #region Major
            CreateMap<Major, GetMajorDto>().ReverseMap();
            CreateMap<Major, UpdateMajorDto>().ReverseMap();
            CreateMap<Major, CreateMajorDto>().ReverseMap();
            CreateMap<Major, MajorDto>().ReverseMap();
            #endregion  
            
            
            #region Question
            CreateMap<Question, GetQuestionDto>().ReverseMap();
            CreateMap<Question, UpdateQuestionDto>().ReverseMap();
            CreateMap<Question, CreateQuestionDto>().ReverseMap();
            CreateMap<Question, QuestionDto>()
                            .ForMember(dest => dest.AnswerName, opt => opt.MapFrom(src => src.Answer.AnswerName))
                            .ForMember(dest => dest.MajorName, opt => opt.MapFrom(src => src.Major.Name));

            #endregion

            #region NPC
            CreateMap<Npc, GetNpcDto>().ReverseMap();
            CreateMap<Npc, UpdateNpcDto>().ReverseMap();
            CreateMap<Npc, CreateNpcDto>().ReverseMap();
            CreateMap<Npc, NpcDto>().ReverseMap();
            #endregion 
            
            #region Location
            CreateMap<Location, GetLocationDto>().ReverseMap();
            CreateMap<Location, UpdateLocationDto>().ReverseMap();
            CreateMap<Location, CreateLocationDto>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
            #endregion  
            
/*            #region Rank
            CreateMap<Rank, GetRankDto>().ReverseMap();
            CreateMap<Rank, UpdateRankDto>().ReverseMap();
            CreateMap<Rank, CreateRankDto>().ReverseMap();
            CreateMap<Rank, RankDto>()
                            .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Event.Name))
                            .ForMember(dest => dest.PlayerName, opt => opt.MapFrom(src => src.Player.Nickname));
            #endregion*/

            #region Answer
            CreateMap<Answer, AnswerDto>().ReverseMap();
                     
            CreateMap<Answer, UpdateAnswerDto>().ReverseMap();
            CreateMap<Answer, GetAnswerDto>().ReverseMap();
            CreateMap<Answer, CreateAnswerDto>().ReverseMap();

            #endregion

            #region Gift
            CreateMap<Gift, GiftDto>().ReverseMap();
            CreateMap<Gift, UpdateGiftDto>().ReverseMap();
            CreateMap<Gift, CreateGiftDto>().ReverseMap();
            CreateMap<Gift, GetGiftDto>().ReverseMap();
            #endregion

            #region Exchange History
            CreateMap<ExchangeHistory, ExchangeHistoryDto>()
                .ForMember(dest => dest.PlayerName, opt => opt.MapFrom(src => src.Player.Nickname))
                .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Item.Name));
            CreateMap<ExchangeHistory, UpdateExchangeHistoryDto>().ReverseMap();
            CreateMap<ExchangeHistory, CreateExchangeHistoryDto>().ReverseMap();
            CreateMap<ExchangeHistory, GetExchangeHistoryDto>().ReverseMap();
            #endregion

            #region Inventory
            CreateMap<Inventory, InventoryDto>()
                  .ForMember(dest => dest.PlayerName, opt => opt.MapFrom(src => src.Player.Nickname));
            CreateMap<Inventory, UpdateInventoryDto>().ReverseMap();
            CreateMap<Inventory, CreateInventoryDto>().ReverseMap();
            CreateMap<Inventory, GetInventoryDto>().ReverseMap();
            #endregion

            #region Item Inventory
            CreateMap<ItemIventory, ItemInventoryDto>()
                .ForMember(dest => dest.ItemName, opt => opt.MapFrom(src => src.Item.Name));
                

            CreateMap<ItemIventory, UpdateItemInventoryDto>().ReverseMap();
            CreateMap<ItemIventory, CreateItemInventoryDto>().ReverseMap();
            CreateMap<ItemIventory, GetItemInventoryDto>().ReverseMap();
            #endregion

            #region Player
            CreateMap<Player, PlayerDto>()
                            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Fullname));
            CreateMap<Player, UpdatePlayerDto>().ReverseMap();
            CreateMap<Player, CreatePlayerDto>().ReverseMap();
            CreateMap<Player, GetPlayerDto>().ReverseMap();
            #endregion  
            
            #region Player History
            CreateMap<PlayHistory, PlayerHistoryDto>()
                            .ForMember(dest => dest.PlayerName, opt => opt.MapFrom(src => src.Player.Nickname))
                            .ForMember(dest => dest.TaskName, opt => opt.MapFrom(src => src.Task.Name)); 
            CreateMap<PlayHistory, UpdatePlayerHistoryDto>().ReverseMap();
            CreateMap<PlayHistory, CreatePlayerHistoryDto>().ReverseMap();
            CreateMap<PlayHistory, GetPlayerHistoryDto>().ReverseMap();
            #endregion
            
            #region Role
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Role, GetRoleDto>().ReverseMap();
            CreateMap<Role, CreateRoleDto>().ReverseMap();
            CreateMap<Role, UpdateRoleDto>().ReverseMap();
            #endregion
            
            #region School
            CreateMap<School, SchoolDto>().ReverseMap();
            CreateMap<School, GetSchoolDto>().ReverseMap();
            CreateMap<School, CreateSchoolDto>().ReverseMap();
            CreateMap<School, UpdateSchoolDto>().ReverseMap();
            #endregion
            
            #region School Event
            CreateMap<SchoolEvent, SchoolEventDto>()
                            .ForMember(dest => dest.SchoolName, opt => opt.MapFrom(src => src.School.Name))
                            .ForMember(dest => dest.EventName, opt => opt.MapFrom(src => src.Event.Name));
            CreateMap<SchoolEvent, GetSchoolEventDto>().ReverseMap();
            CreateMap<SchoolEvent, CreateSchoolEventDto>().ReverseMap();
            CreateMap<SchoolEvent, UpdateSchoolEventDto>().ReverseMap();
            #endregion 
            
          
            
            #region Item
            CreateMap<Item, ItemDto>().ReverseMap();
            CreateMap<Item, GetItemDto>().ReverseMap();
            CreateMap<Item, CreateItemDto>().ReverseMap();
            CreateMap<Item, UpdateItemDto>().ReverseMap();
            #endregion
            

            
            #region User
            CreateMap<User, ApiUserDto>().ReverseMap();
            CreateMap<User, AuthResponseDto>().ReverseMap();
            CreateMap<User, LoginDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<User, GetUserDto>().ReverseMap();
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.Schoolname, opt => opt.MapFrom(src => src.School.Name));

            #endregion


        }
    }
}
