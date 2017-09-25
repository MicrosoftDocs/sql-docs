---
title: Configure SSIS on Linux with ssis-conf | Microsoft Docs
description: This article shows how to configure SQL Server Integration Services on Linux with the ssis-conf utility.
author: douglaslMS
ms.author: douglasl 
manager: craigg
ms.date: 09/26/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: integration-services
ms.assetid: 
---
# Configure SQL Server Integration Services on Linux with ssis-conf

`ssis-conf' is a configuration script that's when you install SQL Server Integration Services (SSIS) for Red Hat Enterprise Linux and Ubuntu. You can use this utility to configure the following properties:

| Command | Description |
|-------------|---------------------------------------------------------------------|
| set-edition | Set the edition of SQL Server                                       |
| telemetry   | Enable or disable SQL Server Integration Services telemetry service |
| setup       | Initialize and setup Microsoft SQL Server Integration Services      |
|||

## How to run ssis-conf

The examples in this article run `ssis-conf` by specify the full path: `/opt/ssis/bin/ssis-conf`. If you navigate to that path before you run `ssis-conf`, you can run the utility in the context of the current directory: `./ssis-conf`.

All listed commands should run as root.

To run these commands with prompts in the language that you prefer, you can specify a locale. For example, to see prompts in Chinese, run the following command:

`sudo LC\_ALL=zh\_CN.UTF-8 /opt/ssis/bin/ssis-conf setup`.

## Use set-edition to set the edition of SQL Server Integration Services

The edition of SSIS is aligned with the edition of SQL Server.

Enter the following command:

`$ sudo /opt/ssis/bin/ssis-conf set-edition`

After you enter the command, you see the following prompt:

```
Choose an edition of SQL Server:

1) Evaluation (free, no production use rights, 180-day limit)

2) Developer (free, no production use rights)

3) Express (free)

4) Web (PAID)

5) Standard (PAID)

6) Enterprise (PAID)

7) Enterprise Core (PAID)

8) I bought a license through a retail sales channel and have a product key to enter.

Details about editions can be found at

https://go.microsoft.com/fwlink/?LinkId=852748&clcid=0x409

Use of PAID editions of this software requires separate licensing through a

Microsoft Volume Licensing program.

By choosing a PAID edition, you are verifying that you have the appropriate

number of licenses in place to install and run this software.

Enter your edition(1-8):
```

If enter a value between 1 and 7, the system configures a free or paid edition. If you enter 8, the utility prompts you to enter the product key that you bought:

```
Enter the 25-character product key:
```

## Use telemetry to configure customer feedback

The `telemetry` command determines whether SSIS sends feedback to Microsoft.

For free editions (that is, Express, Developer, and Evaluation editions), the telemetry service is always enabled. If you have a free edition, you can’t use the `telemetry` command to disable telemetry.

Enter the following command:

`$ sudo /opt/ssis/bin/ssis-conf telemetry`

For paid editions, after you enter the command, you see the following prompt:

```
Send feature usage data to Microsoft. Feature usage data includes information
about your hardware configuration and how you use SQL Server Integration Services.

[Yes/No]:
```

If choose **Yes**, the telemetry service is enabled and starts running. The service auto-starts after each boot. If choose **No**, the telemetry service stops and is disabled.

## Use setup to initialize and setup Microsoft SQL Server Integration Services

Use the `setup` command every time you install SSIS.

Enter the following command:

`sudo /opt/ssis/bin/ssis-conf setup`

The utility prompts you to finish provide acknowledge several items and to provide several values for the following items:
-   Product license
-   EULA agreement
-   Telemetry service
-   The language used by Integration Services

To run the `setup` command with prompts in the language that you prefer, you can specify a locale. For example, to see prompts in Chinese, run: `sudo LC\_ALL=zh\_CN.UTF-8 /opt/ssis/bin/ssis-conf setup`.

## ssis.conf format

The following `/var/opt/ssis/ssis.conf` file provides an example for each setting.

For SQL Server, you can change system settings by changing the values in the `mssql.conf` file. For SSIS, you **cannot** change system settings by changing the values in the `ssis.conf` file. The `ssis.conf` file only shows the results of setup. If you want to change the settings for SSIS, you can delete the `ssis.conf` file and run the `setup` command again.

Here is a sample `ssis.conf` file. Each field corresponds to the result of one setup step.

```
LICENSE          
                       
registered = Y        
                       
pid = enterprisecore  
                       
EULA              
                       
accepteula = Y        
                       
TELEMETRY         
                       
enabled = Y           
                       
language          
                       
lcid = 2052
```