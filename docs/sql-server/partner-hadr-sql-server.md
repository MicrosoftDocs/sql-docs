---
title: SQL Server high availability and disaster recovery partners | Microsoft Docs
description: Lists of third-party partners with solutions to monitor Server.
services: sql-server
ms.topic: conceptual
ms.custom: ""
ms.date: 09/17/2017    
ms.prod: sql
ms.author: mikeray
author: MikeRayMSFT
manager: craigg
---
# SQL Server high availability and disaster recovery partners
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
To provide high availability and disaster recovery for your SQL Server services, choose from a wide variety of industry-leading tools.  This article highlights Microsoft partner companies with high availability and disaster recovery solutions supporting Microsoft SQL Server.

## High availability and disaster recovery partners
<!--|![PartnerShortName][1] |**PartnerShortName**<br>PartnerShortName Brief description of the type of products that partner provides. <br><br>List of supported versions of SQL Server, OS, OS platforms/distros  Server 2005 SP4 - SQL Server 2016 on Windows |[Datasheet][PartnerShortName_datasheet]<br>[Marketplace][PartnerShortName_marketplace]<br>[Website][PartnerShortName_website]<br>[Twitter][PartnerShortName_twitter]<br>[Video][PartnerShortName_youtube]|[![veem_video](./media/partner-hadr-sql-server/PartnerShortName_video.png)](https://www.youtube.com/channel/**************)
-->

| Partner | Description | Links | 
| --- | --- | --- |
|![Azure][5] |**Azure Site Recovery**<br>Site Recovery replicates workloads running on virtual machines or physical servers so that they remain available in a secondary location if the primary site isn't available. You can replicate and fail over SQL Server virtual machines from on-premises data center to Azure or to another on-premises data center or from one Azure data centers to another Azure data center.<br><br> Enterprise and Standard editions of SQL Server 2008 R2- SQL Server 2016|[Website][azure_website]<br>[Marketplace][azure_marketplace]<br>[Datasheet][azure_datasheet]<br>[Twitter][azure_twitter]<br>[Video][azure_youtube]|
|![DH2i][2] |**DH2i**<br>DxEnterprise is Smart Availability software for Windows, Linux & Docker that helps you achieve the nearest-to-zero planned and unplanned downtime, unlocks huge cost savings, drastically simplifies management, and gets you both physical and logical consolidation.<br><br>SQL Server 2005+, Windows Server 2008R2+, Ubuntu 16+, RHEL 7+, CentOS 7+|[Website][dh2i_website]<br>[Datasheet][dh2i_datasheet]<br>[Twitter][dh2i_twitter]<br>[Video][dh2i_youtube]|
|![HPE][4] |**HPE Serviceguard**<br>Protect your critical SQL Server 2017 workloads on Linux ® from unplanned and planned downtime through a multitude of infrastructure and applications faults across physical and virtual environments over any distance with HPE Serviceguard for Linux (SGLX). HPE SGLX A.12.20.00 and later offers context-sensitive monitoring and recovery options for Failover Cluster Instance and Always On Availability Groups SQL Server workloads. Maximize uptime with HPE SGLX without compromising data integrity and performance.<br><br>SQL Server 2017 on Linux - RedHat 7.3, 7.4, SUSE 12 SP2, SP3|[Website][hpe_website]<br>[Datasheet][hpe]<br>[Download Evaluation][hpe_download]<br>[Blog][hpe_download]<br>[Twitter][hpe_twitter]
|![IDERA][3]|**IDERA**<br>SQL Safe Backup is a high-performance backup and recovery solution for SQL Server that saves money by reducing database backup time and backup file size, and by providing instant read and write access to databases within backup files.<br><br>Microsoft SQL Server: 2005 SP1 or later, 2008, 2008 R2, 2012, 2014, 2016; all editions |[Website][idera_website]|
|![NEC][7]|**NEC**<br>ExpressCluster is a comprehensive and fully automated high-availability and disaster recovery solution against all major failures including hardware, software, network, and site failures for SQL Server and associated applications running on physical or virtual machines in on-premises or cloud environments.<br><br>Microsoft SQL Server: 2005 or later; all editions |[Website][necec_website]<br>[Datasheet][necec_datasheet]<br>[Video][necec_youtube]<br>[Download][necec_download]|
|![Portworx][6] |**Portworx**<br>Portworx is the solution for stateful containers running in production. With Portworx, users can manage any database or stateful service on any infrastructure using any container scheduler, including Kubernetes, Mesosphere DC/OS, and Docker Swarm. Portworx solves the five most common problems DevOps teams encounter when running containerized databases and other stateful services in production: persistence, high availability, data automation, support for multiple data stores and infrastructure, and security.<br><br>SQL Server 2017 on Docker |[Website][portworx_website]<br>[Documentation][portworx_docs]<br>[Video][portworx_youtube]|
|![SIOS][8] |**SIOS**<br>SIOS Technology delivers cost-efficient high availability and disaster recovery solutions for SQL Server on Windows or Linux. SIOS SANless clustering eliminates the need for a shared storage SAN, giving you complete flexibility to protect your most important applications in physical, virtual, cloud, and hybrid cloud configurations in single and multi-site environments.<br><br>Add SIOS DataKeeper to your Windows Server Failover Clustering environment to create a SANless volume resource that replaces traditional shared storage making it easy to run WSFC in Azure.<br><br>SIOS Protection Suite is a fully flexible clustering solution that protects critical Linux applications such as SQL Server, SAP, HANA, Oracle, and many others.|[Website][sios_website]<br>[Datasheet][sios_datasheet]<br>[Twitter][sios_twitter]<br>[Marketplace][sios_marketplace]<br>[Video][sios_youtube]|
|![Veeam][1] |**Veeam**<br>Veeam Backup & Replication is a powerful, easy-to-use, and affordable backup and availability solution. It provides fast, flexible, and reliable recovery of virtualized applications and data, bringing VM (virtual machine) backup and replication together in a single software solution. Veeam Backup & Replication delivers award-winning support for VMware vSphere and Microsoft Hyper-V virtual environments.<br><br>SQL Server 2005 SP4 - SQL Server 2016 on Windows |[Website][veeam_website]<br>[Datasheet][veeam_datasheet]<br>[Twitter][veeam_twitter]<br>[Video][veeam_youtube]|

## Next steps
To learn more about additional partners, see [monitoring][mon_partners], [management partners][management_partners], and [development partners][dev_partners].

<!--Image references-->
[1]: ./media/partner-hadr-sql-server/Veeam_green_logo.png
[2]: ./media/partner-hadr-sql-server/dh2i_logo.png
[3]: ./media/partner-hadr-sql-server/idera_logo.png
[4]: ./media/partner-hadr-sql-server/hpe_pri_grn_pos_rgb.png
[5]: ./media/partner-hadr-sql-server/azure_logo.png
[6]: ./media/partner-hadr-sql-server/portworx_logo.png
[7]: ./media/partner-hadr-sql-server/nec_logo.png
[8]: ./media/partner-hadr-sql-server/sios_logo.png

<!--Article links-->
[mon_partners]: ./partner-monitor-sql-server.md
[management_partners]: ./partner-management-sql-server.md
[dev_partners]: ./partner-dev-sql-server.md

<!--Website links -->
[veeam_website]:https://www.veeam.com/
[dh2i_website]:https://dh2i.com
[idera_website]:https://www.idera.com/productssolutions/sqlserver
[hpe_website]: https://www.hpe.com/us/en/product-catalog/detail/pip.376220.html
[azure_website]: https://docs.microsoft.com/azure/site-recovery/site-recovery-sql
[necec_website]: https://www.necam.com/ExpressCluster/
[portworx_website]: https://portworx.com/
[sios_website]: https://us.sios.com/

<!--Get Started Links-->

<!--Datasheet Links-->
[veeam_datasheet]:https://www.veeam.com/veeam_backup_9_5_datasheet_en_ds.pdf
[dh2i_datasheet]:https://dh2i.com/wp-content/uploads/DxE-Win-QuickFacts.pdf
[hpe]:https://www.hpe.com/h20195/v2/default.aspx?cc=us&lc=en&oid=376220
[necec_datasheet]: https://www.necam.com/docs/?id=0d9ef7a7-f935-4909-b6bb-20a47b3
[azure_datasheet]: https://docs.microsoft.com/azure/site-recovery/site-recovery-sql#site-recovery-support
[sios_datasheet]: https://us.sios.com/solutions/high-availability-cluster-software-cloud/

<!--Marketplace Links -->
[azure_marketplace]: https://azuremarketplace.microsoft.com/marketplace/apps?search=site%20recovery&page=1
[sios_marketplace]: https://azuremarketplace.microsoft.com/marketplace/apps/sios_datakeeper.sios-datakeeper-8
<!--Press links-->
<!--[veeam_press]:-->

<!--YouTube links-->
[veeam_youtube]:https://www.youtube.com/user/YouVeeam
[dh2i_youtube]:https://www.youtube.com/user/dh2icompany 
[idera_youtube]:https://www.idera.com/resourcecentral/videos/sql-safe-overview
[azure_youtube]: https://mva.microsoft.com/en-US/training-courses/is-your-lack-of-a-disaster-recovery-site-keeping-you-up-at-night-8680?l=oF7YrFH1_7504984382
[necec_youtube]: https://www.youtube.com/watch?v=9La3Cw1Q1Jk
[portworx_youtube]: https://www.youtube.com/channel/UCSexpvQ9esSRgiS_Q9_3mLQ
[sios_youtube]: https://www.youtube.com/watch?v=U3M44gJNWQE

<!--Twitter links-->
[veeam_twitter]:https://twitter.com/veeam
[dh2i_twitter]:https://twitter.com/dh2i
[hpe_twitter]:https://twitter.com/hpe
[azure_twitter]:https://twitter.com/hashtag/azuresiterecovery
[sios_twitter]:https://www.twitter.com/SIOSTech

<!--Docs links>-->
[portworx_docs]: https://docs.portworx.com/

<!--Download links-->
[hpe_download]: https://h20392.www2.hpe.com/portal/swdepot/displayProductInfo.do?productNumber=SGLX-DEMO
[necec_download]: https://www.necam.com/ExpressCluster/30daytrial/
<!--Blog links-->
[hpe_blog]: https://community.hpe.com/t5/Servers-The-Right-Compute/SQL-Server-for-Linux-Is-Here-and-A-New-Chapter-for-Mission/ba-p/6977571#.WiHWW0xFwUE
