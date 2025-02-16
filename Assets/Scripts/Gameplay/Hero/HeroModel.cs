using System.Collections.Generic;
using Other;
using R3;

public class HeroModel
{
    public ReactiveProperty<float> Health { get; } = new ReactiveProperty<float>(100f);
    public ReactiveProperty<float> Stamina { get; } = new ReactiveProperty<float>(100f);
    public ReactiveProperty<float> Experience { get; } = new ReactiveProperty<float>(0f);
    public ReactiveProperty<float> Speed { get; } = new ReactiveProperty<float>(5f);
    public ReactiveProperty<float> JumpForce { get; } = new ReactiveProperty<float>(5f);
    public ReactiveProperty<bool> IsSprinting { get; } = new ReactiveProperty<bool>(false);

    public ReactiveProperty<List<IItem>> Inventory { get; } = new(new List<IItem>());
}