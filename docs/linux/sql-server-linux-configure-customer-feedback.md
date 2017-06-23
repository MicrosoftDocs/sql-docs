# Configure SQL Server on Linux to send feedback to Microsoft

By default, Microsoft SQL Server collects information about how its customers are using the application. Specifically, SQL Server collects information about the installation experience, usage, and performance. This information helps Microsoft improve the product to better meet customer needs. For example, Microsoft collects information about what kinds of error codes customers encounter so that we can fix related bugs, improve our documentation about how to use SQL Server, and determine whether features should be added to the product to better serve customers.

This document provides details about what kinds of information are collected and about how to configure Microsoft SQL Server on Linux to send that collected information to Microsoft. SQL Server 2017 includes a privacy statement that explains what information we do and do not collect from users. Please read the privacy statement. (link)

Specifically, Microsoft does not send any of the following types of information through this mechanism:
- Any values from inside user tables
- Any logon credentials or other authentication information
- Personally Identifiable Information (PII)

SQL Server 2017 always collects and sends information about the installation experience from the setup process so that we can quickly find and fix any installation problems that the customer is experiencing. SQL Server 2017 can be configured not to send information (on a per-server instance basis) to Microsoft through the SQL Server configuration file

> [!NOTE]
>  You can disable the sending of information to Microsoft only in paid versions of SQL Server. You cannot disable this functionality in Developer, Enterprise Evaluation, and Express editions of SQL Server 2016.

## Disable Customer Feedback
Th CustomerFeedback value in the SQL Server configuration file defines if SQL Server sends feedback to Microsoft. By default, this value is set to true. To change the value, run the following commands:

1. Navigate into the directory with the mssql.conf file:
   ```bash
   sudo cd /var/opt/mssql/
   ```
2. Open the mssql.conf file in your favorite text editor. 

3. Change the value of CustomerFeedback from true to false:
    
    [Telemetry]
    
    CustomerFeedback=false

4. Restart the SQL Server service:

   ```bash
   sudo systemctl restart mssql-server
   ```
