# Configuração de Projeto Blazor C# com Entity Framework Core e PostgreSQL

- Inicializar projeto com o Rider/Visual Studio
- Criar .gitignore
- Instalar as Dependências
- Criar o Modelo de Dados
- Configurar o Contexto da Base de Dados
- Registar o Contexto de Base de Dados
- Configurar a String de Ligação da Base de Dados
- Criar a Base de Dados
- Aplicar as Migrações
- Criar os Controllers
- Utilizar o Controller no Cliente

## Inicializar projeto com o Rider/Visual Studio
  
Para criar um novo projeto Blazor no Rider:

1. "New Solution"

2. Project Type: Web

3. Template: Blazor Web App (interactive render mode: Server)

Nota: Em princípio isto é o mesmo que fazer `dotnet new blazor --interactivity Server`

![Captura de ecrã 2025-03-17 161509](https://github.com/user-attachments/assets/1579c450-12c2-4fd2-b4cb-c103ba30acd0)

  (...) fazer em VS
## Configurar gitignore

O ficheiro `.gitignore` é um arquivo de texto utilizado em projetos que usam o Git para versionamento de código. Ele serve para especificar quais arquivos e diretórios devem ser ignorados pelo Git, ou seja, quais não devem ser rastreados ou incluídos no repositório. Isso é especialmente útil para evitar que arquivos desnecessários, como binários, logs, configurações locais ou dependências, sejam commitados acidentalmente.

template oficial do GitHub para C#: [.gitignore para C#](https://github.com/oktadev/blazor-example/blob/master/.gitignore)

## Instalar as Dependências

Instalar os pacotes NuGet para o Entity Framework Core e PostgreSQL:

```bash
// .net DB 
dotnet add package Microsoft.EntityFrameworkCore

dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL

dotnet add package Microsoft.EntityFrameworkCore.Tools

dotnet add package Microsoft.EntityFrameworkCore.Design

//Swagger

dotnet add package Swashbuckle.AspNetCore

//BlazorBootstrap

dotnet Package Blazor.Bootstrap

```

  ou no IDE usar a interface: e adicionar no projeto as packages:
  ![[Pasted image 20250317161522.png]]
## Criar o Modelo de Dados

Exemplo model

```csharp
public class Jom  
{  
    [Key]  
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
    public int Id { get; set; }  
      
    public string? Label { get; set; } = string.Empty;  
      
    public DateTime Date { get; set; } = DateTime.Now;  
      
    public bool IsDone { get; set; } = false;  
      
    public float TestNumber { get; set; } = 0.0f;  
      
    public DateTime created_at { get; set; } = DateTime.Now;  
      
    public DateTime updated_at { get; set; } = DateTime.Now;  
      
    public DateTime? deleted_at { get; set; }  
      
}

```

## Configurar o Contexto da Base de Dados

Exemplo classe `ApplicationDbContext` que herde de `DbContext`, esta classe é o que "liga" o código à base de dados:

```csharp
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext  
{  
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)  
        : base(options)  
    {  
    }  
      
    public DbSet<Jom> Joms { get; set; } = null!;  
      
    protected override void OnModelCreating(ModelBuilder modelBuilder)  
    {  
        base.OnModelCreating(modelBuilder);  
          
    }
}
```

  Exemplo de implementação de soft delete:
  
  ```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)  
{  
    base.OnModelCreating(modelBuilder);  
      
    // Soft delete  
    foreach (var entityType in modelBuilder.Model.GetEntityTypes())  
    {  
        var deletedAtProperty = entityType.FindProperty("deleted_at");  
        if (deletedAtProperty != null && deletedAtProperty.ClrType == typeof(DateTime?))  
        {  
            var parameter = Expression.Parameter(entityType.ClrType, "e");  
            var property = Expression.Property(parameter, "deleted_at");  
            var nullValue = Expression.Constant(null, typeof(DateTime?));  
            var filter = Expression.Lambda(Expression.Equal(property, nullValue), parameter);  
  
            modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);  
        }  
    }
```

Exemplo de implementação de seeder:

  ```csharp
// Seed data  
modelBuilder.Entity<Jom>().HasData(  
    new Jom   
	{   
        Id = 1,  
        Label = "Joms",   
		Date = DateTime.UtcNow,   
		IsDone = false,   
		TestNumber = 2.5f,   
		created_at = DateTime.UtcNow,   
		updated_at = DateTime.UtcNow,  
        deleted_at = null  
    },  
    new Jom   
	{   
        Id = 2,  
        Label = "Joms2",   
		Date = DateTime.UtcNow,   
		IsDone = true,   
		TestNumber = 7.5f,   
		created_at = DateTime.UtcNow,   
		updated_at = DateTime.UtcNow,  
        deleted_at = null  
    }
```
## Registar o Contexto de Base de Dados

A seguir é preciso configurar a classe que acabamos de criar (ApplicationDbContext) no `Program.cs`, para que o nosso programa "saiba" que ela existe:

Configurar a ApplicationDbContext no file `Program.cs` 

```csharp
// Add db context  
builder.Services.AddDbContext<ApplicationDbContext>(options =>  
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))  
);
```

## Configurar um Database pelo Docker

Instalar docker 

correr no terminal:

```bash
docker run --name es2-db -p 5432:5432 -e POSTGRES_PASSWORD=es2 -e POSTGRES_USER=es2 -e POSTGRES_DB=es2 -d postgres
```

![Captura de ecrã 2025-03-17 163626](https://github.com/user-attachments/assets/06d1d4f7-15f7-42c7-a277-53a35638c4c6)


## Configurar a String de Ligação da Base de Dados

Config the 'DefaultConnection',  string que configura o acesso à base de dados localizada em `appsettings.json`

```json
{

  "ConnectionStrings": {

    "DefaultConnection": "Host=localhost;Database=es2;Username=es2;Password=es2"

  },

  "Logging": {

    "LogLevel": {

      "Default": "Information",

      "Microsoft.AspNetCore": "Warning"

    }

  },

  "AllowedHosts": "*"

}

```

## Migrações

Comandos para realizar migrate

```bash

dotnet ef migrations add InitialCreate

dotnet ef database update

```

ou usar o IDE:

![Captura de ecrã 2025-03-17 163855](https://github.com/user-attachments/assets/eb10573a-ee5c-49c6-a745-1c210ddabdd3)

Ao realizar migration usar nomenclatura decente no seu nome 

![Captura de ecrã 2025-03-17 163954](https://github.com/user-attachments/assets/0c84d546-c524-473f-bd26-f0b3f659f288)


Para realizar update a db selecionar a migration e dar update:

![Captura de ecrã 2025-03-17 164321](https://github.com/user-attachments/assets/27371bd8-7de3-4fc6-90d2-fe3ed243dab3)

Nota: E muito fácil perder o controlo das migration em .net porque se realizarem alguma alteração na db a frame work não vai saber dessa alteração portanto devemos trabalhar sempre pelo codigo e se possivel não fazer nenhuma alteração na DB manual.  
## Controllers

  Exemplo controller base:

```csharp
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
  
[Route("api/[controller]")]  
[ApiController]  
public class JomController : ControllerBase  
{  
    private readonly ApplicationDbContext _context;  
      
    public JomController(ApplicationDbContext context)  
    {  
        _context = context;  
    }
}
```

### DTO model

Os **DTOs (Data Transfer Objects)** são objetos simples utilizados para transportar dados entre diferentes camadas de uma aplicação, como entre a camada api e a camada de controller. Simplificando : São uma versão dos model mais simples onde so temos os atributos necessarios para front end.

```csharp
public class JomResponseDto  
{  
    public int Id { get; set; }  
    public string? Label { get; set; }  
    public DateTime Date { get; set; }  
    public bool IsDone { get; set; }  
    public float TestNumber { get; set; }  
    public DateTime created_at { get; set; }  
    public DateTime updated_at { get; set; }   
      
}
```

### Controller Crud

Listar :
```csharp
// GET: api/Jom  
[HttpGet]  
public async Task<ActionResult<IEnumerable<JomResponseDto>>> GetJoms()  
{  
    var joms = await _context.Joms  
        .Select(jom => new JomResponseDto  
        {  
            Id = jom.Id,  
            Label = jom.Label,  
            Date = jom.Date,  
            IsDone = jom.IsDone,  
            TestNumber = jom.TestNumber,  
            created_at = jom.created_at,  
            updated_at = jom.updated_at  
        })  
        .ToListAsync();  
  
    return Ok(joms);  
}
```

Show:
```csharp
// GET: api/Jom/"id"  
[HttpGet("{id}")]  
public async Task<ActionResult<JomResponseDto>> GetJom(int id)  
{  
    var jom = await _context.Joms.FindAsync(id);  
  
    if (jom == null)  
    {  
        return NotFound();  
    }  
  
    var jomResponse = new JomResponseDto  
    {  
        Id = jom.Id,  
        Label = jom.Label,  
        Date = jom.Date,  
        IsDone = jom.IsDone,  
        TestNumber = jom.TestNumber,  
        created_at = jom.created_at,  
        updated_at = jom.updated_at  
    };  
  
    return Ok(jomResponse);  
}
```

Create:
```csharp
// POST: api/Jom  
[HttpPost]  
public async Task<ActionResult<JomCreateDto>> PostJom(JomCreateDto jomRequest)  
{  
    var jom = new Jom  
    {  
        Label = jomRequest.Label,  
        Date = jomRequest.Date ?? DateTime.UtcNow,  
        IsDone = jomRequest.IsDone,  
        TestNumber = jomRequest.TestNumber,  
        created_at = DateTime.UtcNow,  
        updated_at = DateTime.UtcNow,  
    };  
      
    _context.Joms.Add(jom);  
    await _context.SaveChangesAsync();  
  
    return CreatedAtAction(nameof(GetJom), new { id = jom.Id }, jom);  
}
```

Update:
```csharp
// PUT: api/Jom/"id"  
[HttpPut("{id}")]  
public async Task<IActionResult> PutJom(int id, JomUpdateDto jom)  
{  
    var existingJom = await _context.Joms.FindAsync(id);  
      
    if (existingJom == null)  
    {  
        return NotFound();  
    }  
      
    // Update only the modified properties  
    existingJom.Label = jom.Label ?? existingJom.Label;  
    existingJom.Date = jom.Date != default ? jom.Date : existingJom.Date;  
    existingJom.IsDone = jom.IsDone;  
    existingJom.TestNumber = existingJom.TestNumber != jom.TestNumber ? jom.TestNumber : existingJom.TestNumber;  
    existingJom.updated_at = DateTime.UtcNow;  
      
    try  
    {  
        await _context.SaveChangesAsync();  
    }  
    catch (DbUpdateConcurrencyException)  
    {  
        if (!_context.Joms.Any(e => e.Id == id))  
        {  
            return NotFound();  
        }  
        else  
        {  
            throw;  
        }  
    }  
    return NoContent();  
}
```

Delete:
```csharp
// DELETE: api/Jom/"id"  
[HttpDelete("{id}")]  
public async Task<IActionResult> DeleteJom(int id)  
{  
    var jom = await _context.Joms.FindAsync(id);  
    if (jom == null)  
    {  
        return NotFound();  
    }  
      
    jom.updated_at = DateTime.UtcNow;  
    jom.deleted_at = DateTime.UtcNow;  
      
    await _context.SaveChangesAsync();  
    return NoContent();  
}
```
### Configurar o Swagger

Injetar o controller no `Program.cs`, no builder:

```csharp

// Adicionar controllers de API

builder.Services.AddControllers();
  
// Adicionar serviços do Swagger

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>

{

    c.SwaggerDoc("v1", new OpenApiInfo

    {

        Title = "Sandbox API",

        Version = "v1",

        Description = "API para a aplicação Blazor Sandbox"

    });

});

if (app.Environment.IsDevelopment())

{

    app.UseSwagger();

    app.UseSwaggerUI(c =>

    {

        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sandbox API V1");

    });

}

  

// Mapear controllers

app.MapControllers();

```

### Configurar o BlazorBootstratp

documentation: https://docs.blazorbootstrap.com/getting-started/blazor-webassembly-net-8
#### Add CSS references

After the `<base href="/" />` tag in the **head** section of the **wwwroot/index.html** file, add the following references:

``` 
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous"><link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css" rel="stylesheet" /><link href="_content/Blazor.Bootstrap/blazor.bootstrap.css" rel="stylesheet" />
```

#### Add script references

Insert the following references into the **body** section of the **wwwroot/index.html** file, immediately after the **_framework/blazor.webassembly.js** reference:

```
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script><!-- Add chart.js reference if chart components are used in your application. --><script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/4.0.1/chart.umd.js" integrity="sha512-gQhCDsnnnUfaRzD8k1L5llCCV6O9HN09zClIzzeJ8OJ9MpGmIlCxm+pdCkqTwqJ4JcjbojFr79rl2F1mzcoLMQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><!-- Add chartjs-plugin-datalabels.min.js reference if chart components with data label feature is used in your application. --><script src="https://cdnjs.cloudflare.com/ajax/libs/chartjs-plugin-datalabels/2.2.0/chartjs-plugin-datalabels.min.js" integrity="sha512-JPcRR8yFa8mmCsfrw4TNte1ZvF1e3+1SdGMslZvmrzDYxS69J7J49vkFL8u6u8PlPJK+H3voElBtUCzaXj+6ig==" crossorigin="anonymous" referrerpolicy="no-referrer"></script><!-- Add sortable.js reference if SortableList component is used in your application. --><script src="https://cdn.jsdelivr.net/npm/sortablejs@latest/Sortable.min.js"></script><script src="_content/Blazor.Bootstrap/blazor.bootstrap.js"></script>
```

#### Register services

Add Blazor Bootstrap service in the **Program.cs**

```
builder.Services.AddBlazorBootstrap();
```

Register tag helpers in **_Imports.razor**

```
@using BlazorBootstrap;
```

#### Remove default references

The default Blazor template includes demonstration code and Bootstrap. To remove these components, follow these steps:

1. Delete the **bootstrap** folder from the **wwwroot** directory:
    - Delete the **wwwroot/css/bootstrap** folder.
2. Remove the following line from **wwwroot/index.html** file:
```
<link href="css/bootstrap/bootstrap.min.css" rel="stylesheet" />
```
## Configurar o HttpClient

Serviço HttpClient no `Program.cs` :

```csharp

builder.Services.AddHttpClient();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:PORT") });

```
### Dependencias na Página ou Componente
  
```razor

@inject HttpClient Http

```
### Fazer o Web Request - exemplo 

```csharp
//html
<div class="container mt-4">

    @if (joms == null)

    {
        <p><em>Loading...</em></p>
    }

    else

    {

        <div class="d-flex justify-content-between mb-3">

            <h1>Listar Joms</h1>

            <Button Color="ButtonColor.Primary" @onclick="OnShowAddModalClick">
                 <i class="bi bi-plus-circle"></i> Adicionar Jom
            </Button>
        </div>
        <table class="table table-striped table-bordered table-hover">

            <thead>
                <tr>
                    <th>Id</th>
                    <th>Label</th>
                    <th>Date</th>
                    <th>JomNumber</th>
                    <th>IsDone</th>
                    <th>Ações</th>
                </tr>
            </thead>
            <tbody>
                @foreach (var Jom in joms)
                {
                    <tr>
                        <td>@Jom.Id</td>
                        <td>@Jom.Label</td>
                        <td>@Jom.Date</td>
                        <td>@Jom.TestNumber</td>
                        <td>@Jom.IsDone</td>
                        <td>
                            <button class="btn btn-warning btn-sm" @onclick="() => OpenEditModal(Jom)">
                                <i class="bi bi-pencil-square"></i> Editar
                            </button>
                            <button class="btn btn-danger btn-sm" @onclick="() => OpenDeleteModal(Jom.Id)">
                                <i class="bi bi-trash"></i> Remover
                            </button>
                        </td>
                    </tr>
                }
            </tbody>
        </table>
    }
</div>
````

```csharp
//@code

private List<Jom> joms = new();

protected override async Task OnInitializedAsync()  
{  
    await LoadJoms();  
}  
  
private async Task LoadJoms()  
{  
    var response = await Http.GetFromJsonAsync<List<Jom>>("api/Jom");  
    joms = response ?? new List<Jom>();  
}

````