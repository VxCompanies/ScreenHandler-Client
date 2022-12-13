using Client.Entities;
using Newtonsoft.Json;

namespace Client.Repositories;

internal class FormRepository<TEntity> : IJsonRepository<TEntity>
    where TEntity : EntityBase
{
    private const Environment.SpecialFolder _desktopDirectory = Environment.SpecialFolder.DesktopDirectory;
    private static string _desktopPath = Environment.GetFolderPath(_desktopDirectory);
    private static IList<TEntity> _savedForms;

    static FormRepository()
    {
        var json = File.ReadAllText($"{_desktopPath}/Forms/Forms.json");
        _savedForms = JsonConvert.DeserializeObject<IList<TEntity>>(json) ?? new List<TEntity>();
    }

    public FormRepository()
    {
        do
        {
            try
            {
                File.CreateText($"{_desktopPath}/Forms/Forms.json");
                break;
            }
            catch (Exception)
            {
                System.IO.Directory.CreateDirectory($"{_desktopPath}/Forms");
                continue;
            }
        } while (true);
    }

    public async void Create(TEntity entity)
    {
        if (_savedForms.FirstOrDefault(f => f.Id == entity.Id) is not null)
            throw new Exception($"Could not add a new form. A form with id '{entity.Id}' alredy exists.");

        entity.Id = _savedForms.LastOrDefault()?.Id + 1 ?? 1;

        _savedForms.Add(entity);
        await Save();
    }

    private static async Task Save()
    {
        var json = JsonConvert.SerializeObject(_savedForms);

        using (var file = File.CreateText($"{_desktopPath}/Forms/Forms.json"))
            await file.WriteAsync(json);
    }

    public async void Delete(int id)
    {
        var form = _savedForms.FirstOrDefault(f => f.Id == id);
        if (form is null)
            throw new Exception($"Could not find form with id '{id}'");

        _savedForms.Remove(form);
        await Save();
    }

    public async void Delete(Func<TEntity, bool> filter)
    {
        var form = _savedForms.FirstOrDefault(filter);
        if (form is null)
            throw new Exception("Could not find form with filter.'");

        _savedForms.Remove(form);
        await Save();
    }

    public TEntity Get(int id)
    {
        var form = _savedForms.FirstOrDefault(f => f.Id == id);
        if (form is null)
            throw new Exception($"Could not find form with id '{id}'");

        return form;
    }

    public TEntity Get(Func<TEntity, bool> filter)
    {
        var form = _savedForms.FirstOrDefault(filter);
        if (form is null)
            throw new Exception("Could not find form with filter.'");

        return form;
    }

    public IEnumerable<TEntity> GetAll() => _savedForms;

    public IEnumerable<TEntity> GetAll(Func<TEntity, bool> filter) => _savedForms.Where(filter);

    public async void Update(int id, TEntity newForm)
    {
        if (newForm.Id != id)
            throw new Exception($"Inconsistent data. Id '{id}' is different than new form's id '{newForm.Id}'.");

        var form = _savedForms.FirstOrDefault(f => f.Id == id);
        if (form is null)
            throw new Exception($"Could not find form with id '{id}'");

        var index = _savedForms.IndexOf(form);
        _savedForms[index] = newForm;
        await Save();
    }

    public async void Update(Func<TEntity, bool> filter, TEntity newForm)
    {
        var form = _savedForms.FirstOrDefault(filter);
        if (form is null)
            throw new Exception($"Could not find form with filter.");

        var index = _savedForms.IndexOf(form);
        _savedForms[index] = newForm;
        await Save();
    }
}
