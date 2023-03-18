using Flux.Engine;
using Flux.EntityBehavior;
using Flux.Rendering;
using Microsoft.Extensions.DependencyInjection;
using MinecraftClone;
using Silk.NET.Windowing;

var builder = new GameEngineBuilder("Test engine");

// Add services here
builder.Services
     .AddSilkInput()
     .AddOpenGL<IWindow>()
     .AddImGui()
     .AddResourceServices()
     .AddSingleton<ChunkService>()
     .AddSingleton<TerrainService>()
     .AddModelEntityBuilder();

var engine = builder.Build();

// Add render or update system here
engine.AddOpenGlRendering()
    .AddBehaviorSystem()
    .AddRenderSystem<ChunkRendererSystem>()
    .AddModelRendering()
    .AddUpdateSystem<ChunkSpawnerSystem>();

// Create game logic here


engine.RunWith<Game>();
