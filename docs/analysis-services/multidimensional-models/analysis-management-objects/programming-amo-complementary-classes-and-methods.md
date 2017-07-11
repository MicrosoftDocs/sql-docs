---
title: "Programming AMO Complementary Classes and Methods | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "restores [AMO]"
  - "assemblies [AMO]"
  - "AMO, backup and restore"
  - "capture logs [AMO]"
  - "programming [AMO]"
  - "Analysis Management Objects, backup and restore"
  - "traces [AMO]"
  - "backups [AMO]"
ms.assetid: 14aed554-d2e2-49e5-9c72-26660759bce2
caps.latest.revision: 22
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Programming AMO Complementary Classes and Methods
  This topic contains the following sections:  
  
-   [Assembly Class](#Assembly)  
  
-   [Backup and Restore](#BU)  
  
-   [Trace Class](#TRC)  
  
-   [CaptureLog class and CaptureXML attribute](#CL)  
  
##  <a name="Assembly"></a> Assembly Class  
 Assemblies let users extend the functionality of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] by adding new stored procedures or Multidimensional Expressions (MDX) functions. For more information, see [AMO Other Classes and Methods](../../../analysis-services/multidimensional-models/analysis-management-objects/amo-other-classes-and-methods.md).  
  
 Adding and dropping assemblies is simple and can be performed online. You must be a database administrator to add an assembly to the database or a server administrator to add the assembly to the server object.  
  
 The following sample adds an assembly to the provided database and assigns the service account to run the assembly. If the assembly exists in the database, the assembly is dropped before trying to add it.  
  
```  
static public void CreateStoredProcedures(Database db)  
{  
    ClrAssembly clrAssembly;  
  
    // Verify That assembly exist in database and drop it  
    if (db.Assemblies.ContainsName("StoredProcedures"))  
    {  
        clrAssembly = db.Assemblies.FindByName("StoredProcedures");  
        clrAssembly.Drop();  
    }  
  
    // Create the CLR assembly  
    clrAssembly = db.Assemblies.Add("StoredProcedures");  
    clrAssembly.ImpersonationInfo = new ImpersonationInfo(  
        ImpersonationMode.ImpersonateServiceAccount);  
    clrAssembly.PermissionSet = PermissionSet.Unrestricted;  
  
    // Load the assembly files  
    clrAssembly.LoadFiles(Environment.CurrentDirectory  
        + @"\StoredProcedures2.dll", false);  
  
    clrAssembly.Update();  
}  
  
```  
  
##  <a name="BU"></a> Backup and Restore Methods  
 The Backup and Restore methods let administrators back up databases and restore them.  
  
 The following sample creates backups for all databases in the specified server. If a backup file already exists, then it is overwritten. Backup files are saved in the BackUp folder in the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] Data folder.  
  
```  
static public void BackUpAllDatabases(Server svr)  
{  
    string fileName;  
    if ((svr != null) && ( svr.Connected))  
        foreach (Database db in svr.Databases)  
        {  
            fileName = db.Name + "_" + ((Int64)(DateTime.Today.Year * 10000 + DateTime.Today.Month * 100 + DateTime.Today.Day)).ToString()+ ".abf";  
            db.Backup(fileName, true);  
        }  
}  
```  
  
 The following sample restores the "Adventure Works" backup from the previous sample. If the "My Adventure WorksDW" database already exists, then the database is overwritten.  
  
```  
static public void RestoreAdventureWorks(Server svr)  
{  
    svr.Restore("Adventure Works DW_20051025.abf", "My Adventure WorksDW", true);  
}  
```  
  
##  <a name="TRC"></a> Trace Class  
 Monitoring the server activity requires using two kinds of traces: Session Traces and Server Traces. Tracing the server can tell you how your current task is performing on the server (Session Traces) or the traces can tell you about the overall activity in the server without you even being connected to the server (Server Traces).  
  
 When tracing current activity (Session Traces), the server sends notifications to the current application about the events that are occurring in the server that are caused by the application. Events are captured using event handlers in the current application. You first assign the event handling routines to the <xref:Microsoft.AnalysisServices.SessionTrace> object and then start the Session Trace.  
  
 The following sample shows how to setup a Session Trace to trace current activities. Event handler routines are located at the end of the sample and will output all trace information to the System.Console object. To generate tracing events the "Adventure Works" sample cube will be fully processed after the trace starts.  
  
```  
static public void TestSessionTraces(Server svr)  
{  
    // Start the default trace  
  
    svr.SessionTrace.OnEvent  
        += new TraceEventHandler(DefaultTrace_OnEvent);  
    svr.SessionTrace.Stopped  
        += new TraceStoppedEventHandler(DefaultTrace_Stopped);  
    svr.SessionTrace.Start();  
  
    // Process the databases  
    // The trace handlers will output all progress notifications   
    // to the console  
    Database db = svr.Databases.FindByName("Adventure Works DW Multidimensional 2012");  
    Cube cube = db.Cubes.FindByName("Adventure Works");  
    cube.Process(ProcessType.ProcessFull);  
  
    // Stop the default trace  
    svr.SessionTrace.Stop();  
  
}  
static public void DefaultTrace_OnEvent(object sender, TraceEventArgs e)  
{  
    Console.WriteLine("{0}", e.TextData);  
}  
  
static public void DefaultTrace_Stopped(ITrace sender, TraceStoppedEventArgs e)  
{  
    switch (e.StopCause)  
    {  
        case TraceStopCause.StoppedByUser:  
        case TraceStopCause.Finished:  
            Console.WriteLine("Processing completed successfully");  
            break;  
  
        case TraceStopCause.StoppedByException:  
            Console.WriteLine("Processing failed: {0}",  
                e.Exception.Message);  
            break;  
    }  
}  
```  
  
 Server traces can be configured to log everything to a trace file and can be automatically restarted when the service restarts.  
  
 To set a server trace, you first need to define which events in the server to be monitored, and what data from the event should be saved in the trace file. For each event, you must define the data columns to be saved in the trace file.  
  
 Creating a server trace requires four steps:  
  
1.  Create the server trace object and populate basic common attributes.  
  
     LogFileSize defines the maximum size of the trace file and is defined in MegaBytes; LogFileRollOver enables the logfile to start on a different file if LogFileSize limit is reached, when enabled the file name is appended with a sequence namber; AutoRestart enables the trace to start again if the Service is restarted.  
  
2.  Create the events and the corresponding data columns.  
  
3.  Start and stop the trace as needed.  
  
     Even after the trace has been stopped, the trace exists in the server and should start again if the trace was defined as AutoRestart=**true**.  
  
4.  Drop the trace when no longer needed.  
  
 In the following sample, if the trace already exists, it is dropped and then recreated. Trace files are saved in the Log folder of [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] data folders.  
  
```  
static public void TestServerTraces(Server svr)  
{  
    Trace trc;  
    TraceEvent te;  
  
        trc = svr.Traces.FindByName("TestServerTraces");  
        if (trc != null)  
            trc.Drop();  
        trc = svr.Traces.Add("TestServerTraces", "TestServerTraces");  
        trc.LogFileName = ("TestServerTraces_" +((Int64)(DateTime.Now.Year * 10000 + DateTime.Now.Month * 100 + DateTime.Now.Day)).ToString() + "_" +  
                ((Int64)(DateTime.Now.Hour * 10000 + DateTime.Now.Minute * 100 + DateTime.Now.Second)).ToString() + ".trc");  
        trc.LogFileSize = 100;  
        trc.LogFileRollover = true;  
        trc.AutoRestart = false;  
  
        #region Define Events to trace & data columns per event  
        te = trc.Events.Add(TraceEventClass.ProgressReportBegin);  
        te.Columns.Add(TraceColumn.TextData);  
        te.Columns.Add(TraceColumn.StartTime);  
        te.Columns.Add(TraceColumn.ObjectName);  
        te.Columns.Add(TraceColumn.ObjectPath);  
        te.Columns.Add(TraceColumn.DatabaseName);  
        te.Columns.Add(TraceColumn.NTCanonicalUserName);  
  
        te = trc.Events.Add(TraceEventClass.ProgressReportCurrent);  
        te.Columns.Add(TraceColumn.TextData);  
        te.Columns.Add(TraceColumn.CurrentTime);  
        te.Columns.Add(TraceColumn.ObjectName);  
        te.Columns.Add(TraceColumn.ObjectPath);  
        te.Columns.Add(TraceColumn.DatabaseName);  
  
        te = trc.Events.Add(TraceEventClass.ProgressReportEnd);  
        te.Columns.Add(TraceColumn.TextData);  
        te.Columns.Add(TraceColumn.StartTime);  
        te.Columns.Add(TraceColumn.CurrentTime);  
        te.Columns.Add(TraceColumn.EndTime);  
        te.Columns.Add(TraceColumn.Success);  
        te.Columns.Add(TraceColumn.Error);  
        te.Columns.Add(TraceColumn.ObjectName);  
        te.Columns.Add(TraceColumn.ObjectPath);  
        te.Columns.Add(TraceColumn.DatabaseName);  
        te.Columns.Add(TraceColumn.NTCanonicalUserName);  
        #endregion  
  
        trc.Update();  
        trc.Start();  
  
        #region Process the Adventure Works Cube  
        // The trace settings will output all progress notifications   
        // to the trace file  
        Database db = svr.Databases.FindByName("Adventure Works DW Multidimensional 2012");  
        Cube cube = db.Cubes.FindByName("Adventure Works");  
        cube.Process(ProcessType.ProcessFull);  
        #endregion  
  
        trc.Stop();  
        trc.Drop();  
  
}  
```  
  
##  <a name="CL"></a> CaptureLog and CaptureXml Attributes  
 The CaptureLog attribute enables you to create XMLA batch files from your AMO operations. CaptureLog enables you to script out server objects as databases, cubes, dimensions, mining structures, and others.  
  
 Creating a CaptureLog requires the following steps:  
  
1.  Start capturing the XMLA log by setting the server attribute CaptureXml to **true**.  
  
     This option will start saving all AMO operations to a string collection instead of sending them to the server.  
  
2.  Start AMO activity as usual, but remember that no action is being sent to the server. Activity can be any operation such as processing, creating, deleting, updating, or any other action over an object.  
  
3.  Stop capturing the XMLA log by resetting CaptureXml to **false**.  
  
4.  Review the captured XMLA, either by reviewing each of the strings in the CaptureLog string collection, or by generating a complete string with the ConcatenateCaptureLog method. ConcatenateCaptureLog enables you to generate the XMLA batch as a single transaction and to add the parallel process option to the batch.  
  
 The following sample returns a string with the batch commands to do a Full process on all dimensions and on all cubes on the [Adventure Works DW Multidimensional 2012] database.  
  
```  
static public string TestCaptureLog(Server svr)  
{  
    String capturedXmla = "";  
    if ((svr != null) && (svr.Connected))  
    {  
        svr.CaptureXml = true;  
  
        #region Actions to be captured to an XMLA file  
        //No action is executed during CaptureXml = true  
        Database db = svr.Databases.FindByName("Adventure Works DW Multidimensional 2012");  
        foreach (Dimension dim in db.Dimensions)  
            dim.Process(ProcessType.ProcessFull);  
        foreach (Cube cube in db.Cubes)  
            cube.Process(ProcessType.ProcessFull);  
        #endregion  
  
        svr.CaptureXml = false;  
  
        capturedXmla = svr.ConcatenateCaptureLog(true, true);  
  
    }  
  
    return capturedXmla;  
}  
```  
  
## See Also  
 <xref:Microsoft.AnalysisServices>   
 [Introducing AMO Classes](../../../analysis-services/multidimensional-models/analysis-management-objects/amo-classes-introduction.md)   
 [AMO Other Classes and Methods](../../../analysis-services/multidimensional-models/analysis-management-objects/amo-other-classes-and-methods.md)   
 [Logical Architecture &#40;Analysis Services - Multidimensional Data&#41;](../../../analysis-services/multidimensional-models/olap-logical/understanding-microsoft-olap-logical-architecture.md)   
 [Database Objects &#40;Analysis Services - Multidimensional Data&#41;](../../../analysis-services/multidimensional-models/olap-logical/database-objects-analysis-services-multidimensional-data.md)   
 [Processing a multidimensional model &#40;Analysis Services&#41;](../../../analysis-services/multidimensional-models/processing-a-multidimensional-model-analysis-services.md)  
  
  