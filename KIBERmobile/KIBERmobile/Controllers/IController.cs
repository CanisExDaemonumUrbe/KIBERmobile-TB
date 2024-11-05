using System.Collections.ObjectModel;

namespace KIBERmobile.Controllers;

public interface ICollectionController<T>
{
    public ref ObservableCollection<T> Collection { get; }
    Task LoadDataAsync(int searchId);
    Task<T> GetItemAsync(int itemId);
    void Clear();
}