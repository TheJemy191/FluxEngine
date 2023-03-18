using System.Numerics;
using DefaultEcs;
using Flux.EntityBehavior;
using Flux.MathAddon;
using Flux.Rendering;
using ImGuiNET;

namespace Ocean;

class OceanManager : Behavior, IUIDrawable, IUpdatable
{
    readonly Uniform<Vector2> oceanTilling;
    readonly Entity oceanEntity;
    readonly Entity boatEntity;

    public OceanManager(Uniform<Vector2> oceanTilling, Entity oceanEntity, Entity boatEntity)
    {
        this.oceanTilling = oceanTilling;
        this.oceanEntity = oceanEntity;
        this.boatEntity = boatEntity;
    }

    public void DrawUI(float deltatime)
    {
        ImGui.Begin("OceanSetting");
        {
            var tilling = oceanTilling.value.X;
            ImGui.SliderFloat("Tilling", ref tilling, 0.1f, 10);
            oceanTilling.value = Vector2.One * tilling;
        }
        ImGui.End();
    }

    public void Update(float deltatime)
    {
        ref var oceanTranform = ref oceanEntity.Get<Transform>();
        var boatTranform = boatEntity.Get<Transform>();

        var position = boatTranform.Position;
        position.Y = oceanTranform.Position.Y;
        oceanTranform.Position = position;
    }
}