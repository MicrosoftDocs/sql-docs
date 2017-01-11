---
title: "Data Center Optimization (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions"
ms.custom: na
ms.date: 01/06/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 1c331501-b840-444a-b39d-a33800034672
caps.latest.revision: 3
manager: jeffreyg
---
# Data Center Optimization (DW)---a Technical Reference Guide for Designing Mission-Critical DW Solutions
<?xml version="1.0" encoding="utf-8"?>
<developerConceptualDocument xmlns="http://ddue.schemas.microsoft.com/authoring/2003/5" xmlns:xlink="http://www.w3.org/1999/xlink" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://ddue.schemas.microsoft.com/authoring/2003/5 http://clixdevr3.blob.core.windows.net/ddueschema/developer.xsd">
  <introduction>
    <table xmlns:caps="http://schemas.microsoft.com/build/caps/2013/11">
      <tbody>
        <tr>
          <TD>
            <para>
              <embeddedLabel>Want more guides like this one?</embeddedLabel> Go to <externalLink><linkText>Technical Reference Guides for Designing Mission-Critical Solutions</linkText><linkUri>http://technet.microsoft.com/sqlserver/hh273157</linkUri></externalLink>.</para>
          </TD>
        </tr>
      </tbody>
    </table>
    <para>Companies employ a variety of processes to optimize their data centers to run efficiently and support critical business solutions. These processes range from enacting policies and procedures for change and release management to analyzing how data centers are built and maintained. Data center architecture relies on processes and people working with technology and operations. The objective is to optimize infrastructure, to achieve the business goals, solutions and application success. Enterprises often have a "run book" and other documented processes. For example, the features and discussions in the Microsoft Operations Framework documentation focus on the following:</para>
    <list class="bullet">
      <listItem>
        <para>Processes to plan, deliver, operate, and manage IT</para>
      </listItem>
      <listItem>
        <para>Governance, risk, and compliance activities</para>
      </listItem>
      <listItem>
        <para>Management reviews</para>
      </listItem>
      <listItem>
        <para>Microsoft Solutions Framework best practices</para>
      </listItem>
    </list>
    <para>Data warehouse reference architectures, such as FastTrack and PDW, go a long way towards mitigating DC operations. Historically, customers have had to practically reinvent the wheel for data warehouse applications. Many data warehouses have had to share resources side-by-side with OLTP systems, which is unacceptable for large data warehousing (DW) situations.</para>
  </introduction>
  <section>
    <title>Best Practices</title>
    <content>
      <para>The following resources provide examples of customer scenarios for data center optimization as well as general data center optimization reference material.</para>
      <list class="bullet">
        <listItem>
          <para>Few companies treat their Windows/Microsoft SQL Server systems like their mainframes or other mission critical systems. In many cases they are looked at as a sandbox environment, lacking tight change controls. Putting standardized process and procedures around the Windows and SQL Server environment, and treating it like a mainframe system, is a key to success. </para>
        </listItem>
        <listItem>
          <para>It is worthwhile to understand what vendors a customer is working with, have a preference for, or are looking at evaluating, to understand how this may drive some of their other processes, including database and server maintenance or HA/DR implementations.</para>
        </listItem>
        <listItem>
          <para>The article <externalLink><linkText>Microsoft Operations Framework 4.0</linkText><linkUri>http://technet.microsoft.com/library/cc506049.aspx</linkUri></externalLink><superscript>1</superscript> discusses Microsoft Operations Framework 4.0, and its use in everyday IT practices and activities.</para>
        </listItem>
        <listItem>
          <para>
            <externalLink>
              <linkText>Chapter 1 – Introduction to the Microsoft Solutions Framework</linkText>
              <linkUri>http://technet.microsoft.com/library/bb497060.aspx</linkUri>
            </externalLink>
            <superscript>2</superscript> in the "Solution Guide for Migrating Oracle on UNIX to SQL Server on Windows" describes the Microsoft Solutions Framework, and discusses how it has been used successfully in numerous IT projects.</para>
        </listItem>
        <listItem>
          <para>The article <externalLink><linkText>Microsoft’s Top 10 Business Practices for Environmentally Sustainable Data Centers</linkText><linkUri>http://blogs.technet.com/b/msdatacenters/archive/2009/04/21/microsoft-s-top-10-business-practices-for-environmentally-sustainable-data-centers-celebrating-earth-day.aspx</linkUri></externalLink><superscript>3</superscript> discusses sustainability practices for data centers.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Case Studies and References</title>
    <content>
      <para>The following articles provide examples of how you can optimize your data center:</para>
      <list class="bullet">
        <listItem>
          <para>The article How Microsoft IT Reduces Costs<superscript>4</superscript> describes cost saving techniques.</para>
        </listItem>
        <listItem>
          <para>In the <externalLink><linkText>Introduction</linkText><linkUri>http://technet.microsoft.com/library/cc966505.aspx</linkUri></externalLink><superscript>5</superscript> to the SQL Server 2000 Operations Guide, the Microsoft Operations Framework is briefly discussed and related to the operations tasks and lifecycle of Microsoft SQL Server 2000.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Questions and Considerations</title>
    <content>
      <para>This section provides questions and issues to consider when working with your customers.</para>
      <list class="bullet">
        <listItem>
          <para>Are data center operations handled differently for DW applications, or do they fall under the same "generic" guidelines?</para>
        </listItem>
        <listItem>
          <para>What are operational best practices that the data center, operations, and infrastructure staff subscribe to or have put into practice? Examples might include IT infrastructure library (ITIL), Microsoft Operations Framework, Microsoft Solutions Framework, Sarbanes-Oxley, or their operations run book.</para>
        </listItem>
        <listItem>
          <para>Are there specific goals or business-critical themes that the operations and infrastructure staff are trying to subscribe to (for example, reducing total cost of operation [TCO], reducing power use, or cooling/green initiatives)? Are there concerns or requirements such as HA or DR?</para>
        </listItem>
        <listItem>
          <para>Understand where the data centers are located, how many data centers the company has, and any differences between the data centers. Also consider the networking and security systems and requirements.</para>
        </listItem>
      </list>
    </content>
  </section>
  <section>
    <title>Appendix</title>
    <content>
      <para>Following are the full URLs for the hyperlinked text.</para>
      <para>
        <superscript>1</superscript> Microsoft Operations Framework 4.0  <externalLink><linkText>http://technet.microsoft.com/en-us/library/cc506049.aspx</linkText><linkUri>http://technet.microsoft.com/library/cc506049.aspx</linkUri></externalLink></para>
      <para>
        <superscript>2</superscript> Chapter 1 – Introduction to the Microsoft Solutions Framework  <externalLink><linkText>http://technet.microsoft.com/library/bb497060.aspx</linkText><linkUri>http://technet.microsoft.com/library/bb497060.aspx</linkUri></externalLink></para>
      <para>
        <superscript>3</superscript> Microsoft’s Top 10 Business Practices for Environmentally Sustainable Data Centers  <externalLink><linkText>http://blogs.technet.com/b/msdatacenters/archive/2009/04/21/microsoft-s-top-10-business-practices-for-environmentally-sustainable-data-centers-celebrating-earth-day.aspx</linkText><linkUri>http://blogs.technet.com/b/msdatacenters/archive/2009/04/21/microsoft-s-top-10-business-practices-for-environmentally-sustainable-data-centers-celebrating-earth-day.aspx</linkUri></externalLink></para>
      <para>
        <superscript>4</superscript> How Microsoft IT Reduces Costs <externalLink><linkText /><linkUri /></externalLink><externalLink><linkText>http://technet.microsoft.com/library/bb687783.aspx</linkText><linkUri>http://technet.microsoft.com/library/bb687783.aspx</linkUri></externalLink></para>
      <para>
        <superscript>5</superscript> SQL Server 2000 Operations Guide: Introduction  <externalLink><linkText>http://technet.microsoft.com/library/cc966505.aspx</linkText><linkUri>http://technet.microsoft.com/library/cc966505.aspx</linkUri></externalLink></para>
    </content>
  </section>
  <relatedTopics />
</developerConceptualDocument>