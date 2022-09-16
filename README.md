# Microwave Technical Test

This is the solution to a technical test I completed in 2020.

## Requirements

The requirements are specified in the following [document](https://github.com/jonathan2266/MicrowaveTechnicalTest/blob/master/Technical_Test.pdf).

## Notes

This serves as documentation and explains some of the design choices made.

The solution exists of several libraries that are written in .Net standard 2.0
The tests and the console application are written in .Net Core 2.1.

In the technical test it is stated that "Any additional requirements that may be missed can be assumed by you, but please note those down".
I assumed that the Lights are automatically controlled by the "IMicrowaveOvenHW" interface/hardware. The lights can't be controlled in any other way then by the doors. And this is something that is directly wired into the hardware.

"When I open door heater stops if running."
Calling the interface to stop the heater has no negative effect when the heater was already off. This simplifies the code and the heater state is not known trough the hardware interface.

The given "user stories" where used as a basis for writing the tests. But where not exactly translated into tests.
For example: "When I press start button when door is closed, heater runs for 1 minute"
This statement is verified in multiple tests. In the class "MicrowaveTimerTests" i verify that the timer works.
In the class "MirowaveControllerTests" i verify that the controller flow is correct and that the correct calls were made.
The individual calls where previously verified. You can also take it a step further and write integration tests for the whole system.
But creating tests that run for 1 minute is not ideal.
