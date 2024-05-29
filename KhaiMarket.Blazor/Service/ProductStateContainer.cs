using KhaiMarket.Blazor.Model;

namespace KhaiMarket.Blazor.Service;

public class ProductStateContainer
{
    public List<ProductDTO> Value { get; set; }

    public string SearchValue { get; set; } = "";
    public event Action OnStateChange;
    public void SetValue(List<ProductDTO> value)
    {
        this.Value = value;
        NotifyStateChanged();
    }

    public void SetSearchValue(string value)
    {
        SearchValue = value;
    }

    private void NotifyStateChanged() => OnStateChange?.Invoke();
}