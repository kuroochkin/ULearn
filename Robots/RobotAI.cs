namespace Generics.Robots;

public interface IRobotAI<out TCommand>
{
	public TCommand GetCommand();
}

public class ShooterAI : IRobotAI<IShooterMoveCommand>
{
	int counter = 1;

	public Point Destination { get; set; }

	public IShooterMoveCommand GetCommand()
	{
		return ShooterCommand.ForCounter(counter++);
	}
}

public class BuilderAI : IRobotAI<IMoveCommand>
{
	int counter = 1;

	public Point Destination { get; set; }

	public IMoveCommand GetCommand()
	{
		return BuilderCommand.ForCounter(counter++);
	}
}

public interface IDevice<in TCommand>
{
	public string ExecuteCommand(TCommand command);
}

public class Mover : IDevice<IMoveCommand>
{
	public string ExecuteCommand(IMoveCommand command)
	{
		if (command is null)
			throw new ArgumentException();
		return $"MOV {command.Destination.X}, {command.Destination.Y}";
	}
}

public class ShooterMover : IDevice<IShooterMoveCommand>
{
	public string ExecuteCommand(IShooterMoveCommand command)
	{
		if (command is null)
			throw new ArgumentException();
		var hide = command.ShouldHide ? "YES" : "NO";
		return $"MOV {command.Destination.X}, {command.Destination.Y}, USE COVER {hide}";
	}
}

public class Robot<TCommand>
{
	private readonly IRobotAI<TCommand> ai;
	private readonly IDevice<TCommand> device;

	public Robot(IRobotAI<TCommand> ai, IDevice<TCommand> executor)
	{
		this.ai = ai;
		this.device = executor;
	}

	public IEnumerable<string> Start(int steps)
	{
		for (int i = 0; i < steps; i++)
		{
			var command = ai.GetCommand();
			if (command == null)
				break;
			yield return device.ExecuteCommand(command);
		}
	}
}

public static class Robot
{
	public static Robot<TCommand> Create<TCommand>(IRobotAI<TCommand> ai, IDevice<TCommand> executor)
	{
		return new Robot<TCommand>(ai, executor);
	}
}
