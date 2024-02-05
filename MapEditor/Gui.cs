using System.Linq;
using Godot;
using Unity;

namespace PipeReactor.MapEditor;

public partial class Gui : Node2D
{
	[Export]
	private Control _leftSidebar;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LoadLeftSidebar();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public Control LeftSidebar
	{
		get => _leftSidebar;
		private set => _leftSidebar = value;
	}

	private void LoadLeftSidebar()
	{
		Core.PipeReactor.Container.Registrations
			.Where(registration => registration.RegisteredType == typeof(ImageTexture))
			.ToList().ForEach(registration =>
			{
				var text = registration.Name;
				var flowContainer = new HFlowContainer();
				flowContainer.SizeFlagsHorizontal = Control.SizeFlags.ShrinkCenter;

				var textureRect = new TextureRect();
				textureRect.Texture = Core.PipeReactor.Container.Resolve<ImageTexture>(text);
				textureRect.ExpandMode = TextureRect.ExpandModeEnum.FitHeightProportional;
				textureRect.CustomMinimumSize = new Vector2(128, 0);
				textureRect.SizeFlagsHorizontal = Control.SizeFlags.ExpandFill;
				flowContainer.AddChild(textureRect);

				var lineEdit = new LineEdit();
				lineEdit.Text = text;
				lineEdit.SizeFlagsHorizontal = Control.SizeFlags.ExpandFill;
				lineEdit.Editable = false;
				lineEdit.CustomMinimumSize = new Vector2(128, 0);
				flowContainer.AddChild(lineEdit);

				LeftSidebar.AddChild(flowContainer);
			});
	}
}