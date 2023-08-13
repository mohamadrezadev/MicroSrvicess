namespace MicroServicesMangementAdmin.Front.Models.Dtos;
public record ResultDto(bool IsSuccess,string? Message=null);
public record ResultDto<T>(bool IsSuccess, string? Message=null,T? Data=null) where T :class;