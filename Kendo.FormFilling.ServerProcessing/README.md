# Kendo UI Form Filling with ServerProcessing

## Overview

This project demonstrates PDF form filling capabilities using Kendo UI and submitting the filled data as "byte" directly to the server in an ASP.NET Core environment.

## PDF Form Filling

The Kendo UI for jQuery PDFViewer supports displaying fillable PDF forms and allows users to interact with various input types, such as text boxes, checkboxes, radio buttons, and list boxes. The way of obtaining the fields' data happens with an AnnotationStorage.

## Server Processing

The Kendo PdfViewer uses the pdf.js library under the hood. Submitting filled forms as byte data directly to the server is handled through pdf.js library functionality. This means that extracting the generated PDF file as byte data on the client and saving it on the server is a general pdf.js capability and not specific to Kendo UI.

## Running the Project

To run this project:

```bash
dotnet restore
dotnet build
dotnet run --project Kendo.FormFilling.ServerProcessing/Kendo.FormFilling.ServerProcessing.csproj
```

## Project Structure

```
Kendo.FormFilling.ServerProcessing/
├── Kendo.FormFilling.ServerProcessing.sln
└── Kendo.FormFilling.ServerProcessing/
    ├── Controllers/
    ├── Models/
    ├── Views/
    ├── wwwroot/
    └── ...
```
