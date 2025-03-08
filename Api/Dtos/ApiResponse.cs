namespace Api.Dtos;

public sealed record ApiResponse<T>(
    T? Data,
    bool Success,
    string Message = "",
    string Error = ""
);