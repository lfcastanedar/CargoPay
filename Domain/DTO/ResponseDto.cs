using System.ComponentModel;

namespace Domain.DTO;

public class ResponseDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = null!;
    public object? Result { get; set; }
}

public class GetResponseDTO<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = null!;
    public T? Result { get; set; }
}

public class PayResponseDto
{
    [DefaultValue(true)]
    public bool IsSuccess { get; set; }

    [DefaultValue("Payment completed successfully. The amount paid was {0}, and the remaining card balance is {1}")]
    public string Message { get; set; }

    [DefaultValue("")]
    public string Result { get; set; }
}

