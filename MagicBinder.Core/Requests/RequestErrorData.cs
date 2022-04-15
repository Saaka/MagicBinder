using MagicBinder.Domain.Enums;

namespace MagicBinder.Core.Requests;

public record RequestErrorData(ErrorCode ErrorCode, string? ErrorDetails = null);