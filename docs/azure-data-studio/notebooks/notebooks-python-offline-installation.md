---
title: Python installation in an offline Windows environment
description: This tutorial shows how you can install Python in an offline Windows environment
author: lucyzhang929
ms.author: luczhan
ms.reviewer: erinstellato
ms.date: 07/02/2021
ms.service: azure-data-studio
ms.topic: how-to
ms.custom: intro-installation
---

# Install Python in an offline Windows environment

This tutorial demonstrates how to install and use the Python kernel in an offline Windows environment with notebooks.

## Prerequisites

- [Azure Data Studio installed](../download-azure-data-studio.md)

## Download Python and Dependencies

1. On a machine that has internet access, download the latest Azure Data Studio Python package from here: [https://go.microsoft.com/fwlink/?linkid=2163338](https://go.microsoft.com/fwlink/?linkid=2163338). Unzip the file into a local directory (for example: C:\\azuredatastudio-python). 
   >[!Note]
   >The latest Azure Data Studio Python version is 3.8.10.

2. In a terminal, navigate to the Python directory.

    ```cmd
    cd C:\azuredatastudio-python
    ```

3. Create a text file named **requirements.txt** with the following contents.

    ```txt
    pandas>=0.24.2
    jupyter>=1.0.0
    sparkmagic>=0.12.9
    powershell-kernel>=0.1.3
    ```

4. Create a sub directory named **`wheelhouse`**.

    ```cmd
    mkdir wheelhouse
    ```

5. Run the following command to download the required dependencies to the sub directory.

    ```cmd
    python.exe -m pip download -r requirements.txt -d wheelhouse
    ```

## Install Python on a machine that doesn't have internet access

1. On a machine that doesn't have internet access, copy the Python folder from above to a local directory (for example: C:\\azuredatastudio-python).

2. In a terminal, navigate to the Python folder.

    ```cmd
    cd C:\azuredatastudio-python
    ```

3. Run the following to install the dependencies.

    ```cmd
    python.exe -m pip install -r requirements.txt --no-index --find-links wheelhouse
    ```

## Use the Python Installation in Azure Data Studio

1. Open Azure Data Studio
2. From the Command Palette, search for Configure Python for Notebooks.
3. In the Configure Python for Notebooks wizard, select *Use existing Python installation*, and browse to the installed Python location (for example: C:\\azuredatastudio-python).

Once the wizard is completed, open a new notebook and change the kernel to Python.

> [!Note]
> The steps above can be repeated for any additional packages needed.
