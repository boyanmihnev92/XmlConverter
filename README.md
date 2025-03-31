XML Converter (.NET 8 + Blazor + Clean Architecture)

Overview

This is a simple full-stack application built using .NET 8, Blazor, and Clean Architecture. The application allows users to upload an XML file and convert it into a JSON file. The file is then offered as a download.

The solution follows Clean Architecture principles and implements the Strategy Pattern for file conversion. This makes it easy to expand the system in the future to support other formats like CSV, YAML, or Excel.

Features

Upload an XML file via a user-friendly Blazor Web UI.

Convert the uploaded file into JSON.

Download the converted JSON file.

Error handling and file validation.

Clean Architecture separation with MediatR and CQRS.

Strategy + Factory pattern for scalable conversion logic.

Projects in the Solution

XmlConverter.Api: The Web API backend using ASP.NET Core and MediatR.

XmlConverter.UI: The Blazor WebAssembly frontend.

XmlConverter.Application, XmlConverter.Domain, XmlConverter.Infrastructure: Core layers of the Clean Architecture.

Getting Started

1. Clone the Repository

git clone https://github.com/boyanmihnev92/XmlConverter.git

2. Open the Solution in Visual Studio

Open XmlConverter.sln

Right-click the solution and choose Properties.

Under Common Properties > Startup Project, choose Multiple startup projects.

Set XmlConverter.Api and XmlConverter.UI to Start.

3. Run the Application

Press F5 or click Start in Visual Studio.

The API and UI projects will both launch.

The Blazor UI should open in your browser.

Usage

Navigate to the "Process XML file" page.

Upload an .xml file.

The app will convert the file and provide a .json file for download.

Architecture & Extensibility

The file conversion logic uses the Strategy Pattern.

The current implementation supports XML -> JSON.

To add support for new formats (e.g., CSV), simply:

Create a new class implementing IFileConvertStrategy.

Register it in the IFileConvertionFactory.

Implement the feature in the ConvertFile method to convert the XML document to the required end format

Tech Stack

.NET 8

Blazor WebAssembly

MediatR

Refit for HTTP communication

Strategy + Factory Pattern for conversions

License

MIT License (or your preferred license)

Contributions

Feel free to fork and submit a pull request if you'd like to contribute!