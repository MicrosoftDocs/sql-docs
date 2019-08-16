If you use SQL Server Integration Services (SSIS) and want to migrate your SSIS projects/packages from projects/packages to the destination Azure Data Factory (ADF) SSIS Integration Runtime with Azure SQL Database or Azure SQL Database managed instance, you can select "Integration Service" assessment type in DMA tool to assess source SSIS projects/packages.

The assessment helps to discover issues that can affect the migration from on-premises SQL Server Integration Services to ADF SSIS Integration Runtime. These are described as compatibility issues and are organized in the following categories:
    - Migration blockers: discovers the compatibility issues that block migrating source projects/packages to ADF SSIS Integration Runtime. DMA provides recommendations to   
                          help you address those issues.
    - Information issues: detects partially supported or deprecated features that are used in source projects/packages.

DMA provides a comprehensive set of recommendations, alternative approaches available in Azure, and mitigating steps so that you can incorporate them into your migration projects. 

The assessment of packages hosted in File system is supported in this release. The assessment of packages hosted in MSDB or Package Store, projects hosted in SSISDB will be supported in later release.

References:
Migrate SQL Server Integration Services packages to an Azure SQL Database managed instance: 
https://docs.microsoft.com/en-us/azure/dms/how-to-migrate-ssis-packages-managed-instance

Redeploy SQL Server Integration Services packages to Azure SQL Database:
https://docs.microsoft.com/en-us/azure/dms/how-to-migrate-ssis-packages
