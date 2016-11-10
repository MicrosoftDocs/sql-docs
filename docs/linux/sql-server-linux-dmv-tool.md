---
# required metadata

title: Monitor SQL Server on Linux with dmvtool - SQL Server vNext CTP1 | Microsoft Docs
description: This topic describes how to monitor SQL Server running on Linux with the dmvtool.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 11/09/2016
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 2856cc89-d35b-4030-b12e-4c4ff9240706

# optional metadata

# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---
# Monitor SQL Server on Linux with dmvtool

## Background

[*Dynamic Management Views*](https://msdn.microsoft.com/en-us/library/ms188754.aspx) (DMVs) are a useful tool to monitor the health of a SQL Server instance, diagnose problems, and tune performance. Traditionally, to get this information, you would use GUI admin tools or command line tools to run queries.

DMVTool is a utility that lets you view SQL Servers DMVs on a Linux machine as a set of virtual CSV and JSON files in a mounted virtual directory. All you need to do is view the contents of the virtual files in the mounted virtual directory to see the same data you would see as if you ran a SQL query to view the DMV data. There is no need to login into the SQL Server using a GUI or command line tool or run SQL queries. DMVTool can also be used in scenarios where you want to access DMV data from the context of a script.

DMVTool uses the FUSE file system module to create two zero byte files for each DMV – one for showing the data in CSV format and one for showing the data in JSON format. When a file is “read”, the relevant information from the corresponding DMV is queried from SQL Server and displayed just like the contents of any CSV or JSON text file. 
 
> [!NOTE]
> Viewing the contents of DMVs in JSON format is only available when connected to a SQL Server 2016 or higher version of SQL Server. DMVTool can connect to local SQL Servers running on Linux and remote SQL Servers running on Windows or Linux. For now, DMVTool is only a Linux-based tool.

## 1. Install DMVTool


TODO: Find Install steps for Public Preview 
### To install on RHEL
### To install on Ubuntu

## 2. Configure DMVTool

The DMV tool needs a config file and an empty directory to mount and create the virtual files in. The config file allows you to specify the hostname, username, password (optionally), and SQL Server version for all the servers you want to manage using DMVTool. The version attribute needs to be 14 (SQL Server 2014) or 16 (SQL Server 2016 or higher).

1. Change directory to a directory where you want to create your config file and mounting directory.

2. Create a directory you want to mount to

        $ mkdir dmv

3.  Create a file to store the configuration

        $ touch dmvtool.config

4.  Edit the config file using an editor like VI

        $ vi dmvtool.config

The config file should be in the following format: 
    [server friendlyname] 
    hostname=&lt;HOSTNAME&gt; 
    username=&lt;USERNAME&gt;
    password=&lt;PASSWORD&gt; 
    version=&lt;VERSION&gt;

Password is an **optional** field. If it is not specified, the tool will
prompt you to input your password when it tries to connect.

There can be multiple servers in the config file with different section
names. 

Example:

    [server1]
    hostname= 10.82.164.188 
    username=sa 
    password=password1 
    version=16

    [server2]
    hostname= 10.82.164.127 
    username=sa 
    password=password2 
    version=16

## 3. Run DMVTool

    dmvtool -c ./[Config File] -m ./[Mount Directory]

Example:

    dmvtool -c ./dmvtool.config -m ./dmv

The previously empty mount directory will now have directories corresponding to the SQL Server(s) listed in the config file. Navigating through the folders you can see the files corresponding to the DMVs. You will also see the .json files in folders that are connected to SQL Server 2016 or higher.

Example:

    $ cd dmv
    $ ls

 You should see the list of your server friendly names.

    $ cd <server friendly name>
    $ ls

You should see the list of DMVs as files. Look at the contents of one of the files:

    $ more <dmv file name>

You can pipe the output from DMVTool to tools like [*cut*](https://en.wikipedia.org/wiki/Cut_(Unix)) (CSV) and [*jq*](https://stedolan.github.io/jq/) (JSON) to format the data for better readability.

## 4. Stop DMVtool 

By default, DMVTool runs in background. You can shut it down using the following commands: 

    ps -A | grep dmvtool

    kill -2 <dmvtool pid>;

If you want to run it in the foreground you can pass the -f parameter. You can pass the -v parameter for verbose output if you are running the tool in the foreground.

## Additional Notes

- The attribution file and license for DMVtool can be found in the directory **/usr/share/doc/dmvtool/.**

- The DMVTool runs in the background unless you pass the -f parameter.

## Troubleshooting

- **Error message**: Will not run as root to avoid security errors
    - **Resolution**: Run dmvtool as a non-root user.

- **Error message**: Transport endpoint is not connected 
    - **Resolution**: Run fusermount -u \[Mount directory\]

- **Error message**: FUSE is not installed.
    - **Resolution**: Install FUSE package (e.g. apt-get install fuse or yum install fuse)

- **Error message**: Insufficient permissions
    - **Resolution**: Make sure that the user running DMVTool has read/write permission to /dev/fuse.
