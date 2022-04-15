using MagicBinder.Domain.Enums;

namespace MagicBinder.Core.Requests.Models;

public record RequestErrorData(ErrorCode ErrorCode, string? ErrorDetails = null);