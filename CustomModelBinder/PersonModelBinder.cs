using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ControllerExamples.CustomModelBinder;

public class PersonModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        Person person = new Person();
        if(bindingContext.ValueProvider.GetValue("FirstName").Length > 0)
            person.PersonName = bindingContext.ValueProvider.GetValue("FirstName").FirstValue;
        if (bindingContext.ValueProvider.GetValue("LastName").Length > 0)
            person.PersonName += string.Concat(" ", bindingContext.ValueProvider.GetValue("LastName").FirstValue);

        person.Email = bindingContext.ValueProvider.GetValue("Email").FirstValue;
        person.Age = Convert.ToInt32(bindingContext.ValueProvider.GetValue("Age").FirstValue);
        person.DateOfBirth = Convert.ToDateTime(bindingContext.ValueProvider.GetValue("DateOfBirth").FirstValue);
        person.Id = bindingContext.ValueProvider.GetValue("Id").FirstValue;
        person.Password = bindingContext.ValueProvider.GetValue("Password").FirstValue;
        person.ConfirmPassword = bindingContext.ValueProvider.GetValue("ConfirmPassword").FirstValue;
        person.FromDate = Convert.ToDateTime(bindingContext.ValueProvider.GetValue("FromDate").FirstValue);
        person.ToDate = Convert.ToDateTime(bindingContext.ValueProvider.GetValue("ToDate").FirstValue);
        person.Phone = bindingContext.ValueProvider.GetValue("Phone").FirstValue;

        if (person.Age.Equals(0))
            person.Age = Convert.ToInt32((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365);

        bindingContext.Result = ModelBindingResult.Success(person);

        return Task.CompletedTask;

    }
}

