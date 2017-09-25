---
title: Schedule SSIS packages on Linux with cron | Microsoft Docs
description: This article shows how to schedule SQL Server Integration Services packages on Linux with the cron service.
author: douglaslMS
ms.author: douglasl 
manager: craigg
ms.date: 09/26/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: integration-services
ms.assetid: 
---
# Schedule SQL Server Integration Services package execution on Linux with cron

When you run SQL Server Integration Services (SSIS) and SQL Server on Windows, you can automate the execution of SSIS packages by using SQL Server Agent. When you run SQL Server and SSIS on Linux, however, the SQL Server Agent utility isn't available to schedule jobs on Linux. Instead you use **Cron** service that's widely used on Linux platforms to automate package execution.

This article provides examples that show how to automate the execution of SSIS packages. The examples were written to run on Red Hat Enterprise. The code is similar for other Linux distributions like Ubuntu.

## Prerequisites

Before you can use the Cron service to run jobs, you have to check whether the Cron service is running on your computer.

Use following command to check the status of the Cron service.

`systemctl status crond.service`

If the service is not active (that is, not running), consult your administrator to set up and configure the Cron service properly.

## Create Jobs

A Cron job is a task that you can configure to run regularly at a specified interval. The job can be as simple as a command that you would normally type directly in the console or run as a shell script.

For easy management and maintenance purposes, it's recommended to put your package execution commands into a script with a descriptive name.

Here is a simple example of a shell script that contains only single command to run a package. You can add more commands as required.

```
# A simple shell script that contains a simple package execution command
# Script name: SSISpackageName.daily

/opt/ssis/bin/dtexec /F yourSSISpackageName.dtsx >> $HOME/tmp/out 2>&1
```

## Schedule Jobs with the Cron Service

After you have defined your jobs, you can schedule them to run automatically by using the Cron service.

To add your job for Cron to run, you have to add the job in the `crontab` file. To open the `crontab` file in an editor where you can add or update the job, use the following command.

`crontab -e`

To schedule the previously described job to run daily at 2:10 a.m., add following line to the `crontab` file.

```
# run SSIS package Name at 2:10AM every day
10 2 \* \* \* $/HOME/SSIS/jobs/SSISpackageName.daily
```

Save the `crontab` file and quit the editor.

To understand the format of the sample command, review the information in the following section.
 
## Format of a Crontab file

The following image shows the format description of the job line added to the `crontab` file.

![](ssis-linux-cron-job-definition.png)

To get a more detailed description of the `crontab' file format, use the following command.

`man 5 crontab`

Here's an example of the output:

![](ssis-linux-cron-crontab-format.png)
