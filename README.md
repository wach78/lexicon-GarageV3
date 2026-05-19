# GarageV3

GarageV3 is a C# console application for managing a small vehicle garage.

This version continues from GarageV2 and focuses on improving structure, readability, validation, and testability. A key goal for GarageV3 is to add automated unit tests for the core garage logic and validation rules.

## Project status

GarageV3 is currently a learning project for object-oriented programming in C#.

Current focus:

- Keep the console application working.
- Improve the project structure step by step.
- Add unit tests for important business logic.
- Make the code easier to maintain and extend.

## Features

The application lets the user:

- Create a garage with a custom capacity.
- Populate the garage with predefined sample vehicles.
- Park new vehicles.
- Prevent duplicate plate numbers.
- Detect when the garage is full.
- List all parked vehicles.
- Show counts grouped by vehicle type.
- Remove a vehicle by plate number.
- Find a vehicle by plate number.
- Search vehicles by:
  - Vehicle type
  - Color
  - Number of wheels

## Vehicle types

The application currently supports:

| Vehicle type | Extra data |
|---|---|
| Car | Fuel type |
| Motorcycle | Cylinder volume |
| Bus | Number of seats |
| Boat | Length |
| Airplane | Number of engines |

All vehicles share common data:

- Plate number
- Color
- Number of wheels

## Project structure

```text
lexicon-GarageV3/
├── Enums/
│   ├── AddVehicleResult.cs
│   ├── FuelType.cs
│   ├── MenuChoice.cs
│   ├── SearchVehicleTypes.cs
│   ├── VehicleColor.cs
│   └── VehicleTypeChoice.cs
├── Interfaces/
│   ├── IConsoleInputReader.cs
│   ├── IConsoleMenu.cs
│   ├── IConsoleOutputWriter.cs
│   ├── IConsoleUI.cs
│   ├── IGarageHandler.cs
│   ├── IVehicle.cs
│   └── IVehicleInputValidator.cs
├── Moduls/
│   ├── AirPlane.cs
│   ├── Boat.cs
│   ├── Bus.cs
│   ├── Car.cs
│   ├── CommonVehicleData.cs
│   ├── Garage.cs
│   ├── GarageHandler.cs
│   ├── MotorCycle.cs
│   └── Vehicle.cs
├── UI/
│   ├── ConsoleInputReader.cs
│   ├── ConsoleMenu.cs
│   ├── ConsoleOutputWriter.cs
│   └── ConsoleUI.cs
├── Validator/
│   └── VehicleInputValidator.cs
├── GarageV3.csproj
├── GarageV3.slnx
└── Program.cs
```

## Architecture overview

| Area | Responsibility |
|---|---|
| `Program.cs` | Starts the application and wires dependencies together. |
| `ConsoleUI` | Controls the main user flow and menu actions. |
| `ConsoleMenu` | Builds menu text shown to the user. |
| `ConsoleInputReader` | Reads and parses user input. |
| `ConsoleOutputWriter` | Writes normal output, error messages, and result messages. |
| `VehicleInputValidator` | Validates plate numbers and numeric vehicle input. |
| `GarageHandler` | Acts as the main service layer between the UI and the garage. |
| `Garage<T>` | Stores parked vehicles and handles add, remove, find, list, and search operations. |
| `Vehicle` and derived classes | Represent the domain model for different vehicle types. |
| `Enums` | Define menu choices, colors, fuel types, vehicle types, and add-result states. |
| `Interfaces` | Define contracts for UI, input, output, validation, garage handling, and vehicles. |

## Requirements

- .NET SDK that supports `net10.0`
- A terminal or IDE that can run .NET console applications

## How to run

Clone the repository:

```bash
git clone https://github.com/wach78/lexicon-GarageV3.git
cd lexicon-GarageV3
```

Run the application:

```bash
dotnet run
```

## How to build

```bash
dotnet build
```

## Unit tests

GarageV3 is intended to include unit tests.

The recommended next step is to add a separate test project, for example:

```text
GarageV3.Tests/
├── GarageTests.cs
├── GarageHandlerTests.cs
└── VehicleInputValidatorTests.cs
```

Recommended test areas:

| Test area | Examples |
|---|---|
| `VehicleInputValidator` | Plate number validation, wheel limits, boat length, seats, cylinder volume, engine count |
| `Garage<T>` | Add vehicle, remove vehicle, find by plate number, full garage, duplicate plate number |
| `GarageHandler` | Create garage, populate garage, park vehicle result handling, search behavior |
| Vehicle models | Constructor validation and shared vehicle data |

Example commands after a test project has been added:

```bash
dotnet test
```

## Suggested test project setup

One possible setup is xUnit:

```bash
dotnet new xunit -n GarageV3.Tests
dotnet sln GarageV3.slnx add GarageV3.Tests/GarageV3.Tests.csproj
dotnet add GarageV3.Tests/GarageV3.Tests.csproj reference GarageV3.csproj
dotnet test
```

If `GarageV3.slnx` causes problems with `dotnet sln`, create a standard solution file instead:

```bash
dotnet new sln --name GarageV3
dotnet sln GarageV3.sln add GarageV3.csproj
dotnet sln GarageV3.sln add GarageV3.Tests/GarageV3.Tests.csproj
dotnet test
```

## Main menu

When the application starts, the user can choose between the following actions:

| Choice | Action |
|---:|---|
| 1 | Create garage |
| 2 | Populate garage with vehicles |
| 3 | List all parked vehicles |
| 4 | List vehicle types and count |
| 5 | Park a vehicle |
| 6 | Remove a vehicle |
| 7 | Find vehicle by plate number |
| 8 | Search vehicles |
| 0 | Exit |

## Validation rules

The application validates user input before creating or searching vehicles.

| Field | Rule |
|---|---|
| Plate number | 6 characters, only letters A-Z and numbers 0-9 |
| Number of wheels | 0-20 |
| Boat length | 1-100 |
| Number of seats | 1-100 |
| Cylinder volume | 50-2500 |
| Number of engines | 1-12 |

Plate numbers are normalized to uppercase when read from the console.

## Sample vehicles

The `Populate` action adds predefined sample vehicles, for example:

| Vehicle | Plate number | Color | Extra data |
|---|---|---|---|
| Car | ABC123 | Red | Gasoline |
| Car | ABC321 | Green | Diesel |
| Motorcycle | MCC123 | Black | 600 cc |
| Bus | BUS123 | Blue | 45 seats |
| Boat | BOA123 | White | Length 8 |
| Airplane | AIR123 | Silver | 2 engines |

## Current limitations

- The application is console-only.
- Data is stored in memory and is lost when the application exits.
- There is no database or file persistence.
- Unit tests are planned for GarageV3, but the test project may still need to be added.
- No license file is currently included.
- The project folder is named `Moduls`; this appears to mean `Models` or `Modules`.

## Possible future improvements

- Add a dedicated unit test project.
- Add more unit tests for validation, garage operations, and search behavior.
- Add persistence to JSON, SQLite, or another storage format.
- Rename `Moduls` to a clearer name such as `Models` or `Domain`.
- Add stronger separation between domain logic and console UI.
- Replace raw dictionaries with strongly typed return models where useful.
- Add sorting options when listing vehicles.
- Add more flexible search filters.
- Add XML documentation comments for public interfaces and classes.
- Add a license file if the project should be shared publicly.

## Purpose

This project is useful for practicing:

- Classes and objects
- Inheritance
- Interfaces
- Enums
- Generics
- Collections
- LINQ
- Input validation
- Console UI
- Basic service layering
- Separation of concerns
- Unit testing
