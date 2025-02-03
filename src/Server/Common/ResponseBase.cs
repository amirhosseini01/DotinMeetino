namespace Server.Common;

public static class ResponseBase
{

	public static ResponseDto<T> Failed<T>(string message, T? obj = default)
	{
		return FailResponse<T>(messages: [message], obj: obj);
	}

	public static ResponseDto<T> Failed<T>(List<string> message, T? obj = default)
	{
		return FailResponse<T>(messages: message, obj: obj);
	}

	public static ResponseDto<T> Success<T>(T? obj = default)
	{
		return SuccessResponse<T>(obj: obj);
	}

	public static ResponseDto<T> Success<T>(string message, T? obj = default)
	{
		return SuccessResponse<T>(messages: [message], obj: obj);
	}

	public static ResponseDto<T> Success<T>(List<string> message, T? obj = default)
	{
		return SuccessResponse<T>(messages: message, obj: obj);
	}

	private static ResponseDto<T> FailResponse<T>(List<string>? messages = null, T? obj = default)
	{
		return new() { IsFailed = true, IsSuccess = false, Messages = messages, Obj = obj };
	}

	private static ResponseDto<T> SuccessResponse<T>(List<string>? messages = null, T? obj = default)
	{
		return new() { IsFailed = false, IsSuccess = true, Messages = messages, Obj = obj };
	}
}

public class ResponseDto<T>
{
	public bool IsSuccess { get; set; }
	public bool IsFailed { get; set; }
	public List<string>? Messages { get; set; }
	public T? Obj { get; set; }
	
	//todo: use enum or ... instead of int
	public int? ErrorCode { get; set; }
	
	public static implicit operator bool(ResponseDto<T> response)
	{
		return response.IsSuccess;
	}
	
	public static bool operator !(ResponseDto<T> response)
	{
		return !response.IsSuccess;
	}
}
