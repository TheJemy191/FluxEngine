using Flux.Engine;
using Flux.EntityBehavior;
using Flux.Rendering;
using Ocean;
using Silk.NET.Windowing;

var builder = new GameEngineBuilder("Test engine");

// Add services here

builder.Services
    .AddSilkInput()
    .AddOpenGL<IWindow>()
    .AddImGui()
    .AddResourceServices()
    .AddBehaviorServices()
    .AddModelEntityBuilder();

var engine = builder.Build();

// Add render or update system here

engine.AddOpenGlRendering()
    .AddModelRendering()
    .AddImGuiRendering()
    .AddBehaviorSystem();

// Add game logic here


engine.RunWith<Game>();
