namespace PetStore.Blazor.WASM.Shared.Helpers
{
    public class GetProperty
    {
        public static object Value(object model, string propertyName)
        {
            return model.GetType().GetProperty(propertyName).GetValue(model, null);
        }
    }
}