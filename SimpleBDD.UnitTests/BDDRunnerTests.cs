﻿using NUnit.Framework;
using Rhino.Mocks;
using SimpleBDD.Notification;

namespace SimpleBDD.UnitTests
{
	[TestFixture]
	public class BDDRunnerTests
	{
		private BDDRunner _subject;

		[SetUp]
		public void SetUp()
		{
			_subject = new BDDRunner();
		}

		[Test]
		public void Should_use_console_progress_by_default()
		{
			Assert.That(_subject.ProgressNotifier, Is.InstanceOf<ConsoleProgressNotifier>());
		}

		[Test]
		public void Should_display_scenario_name()
		{
			var progressNotifier = MockRepository.GenerateMock<IProgressNotifier>();
			_subject.ProgressNotifier = progressNotifier;
			_subject.RunScenario();
			progressNotifier.AssertWasCalled(n => n.NotifyScenarioStart("Should display scenario name"));
		}

		[Test]
		public void Should_display_steps()
		{
			var progressNotifier = MockRepository.GenerateMock<IProgressNotifier>();
			_subject.ProgressNotifier = progressNotifier;
			_subject.RunScenario(Step_one, Step_two);
			progressNotifier.AssertWasCalled(n => n.NotifyStepStart("Step one", 1, 2));
			progressNotifier.AssertWasCalled(n => n.NotifyStepStart("Step two", 2, 2));
		}

		void Step_one() { }
		void Step_two() { }
	}
}