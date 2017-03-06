# Image Resize Function

The easiest way to resize images stored in Blob Storage (on Azure); uses [ImageProcessor](http://imageprocessor.org/)

## Quick Deploy to Azure

[![Deploy to Azure](http://azuredeploy.net/deploybutton.svg)](https://azuredeploy.net/)

See below for application setting values.

## Application settings

Here are the app settings that the function app depends on:

- **ImageRepository** - The storage account which houses the images to be processed; and where the resized images are stored.

## Running Locally

Visual Studio function app project is included. To run locally, create an `appsettings.json` file in the root of the function app. There is a sample included.