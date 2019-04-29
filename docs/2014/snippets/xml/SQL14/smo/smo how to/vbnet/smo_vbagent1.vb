Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Imports Microsoft.SqlServer.Management.Smo.Agent
Module SMO_VBAgent1

    Sub Main()
        ' <snippetSMO_VBAgent1>
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Define an Operator object variable by supplying the Agent (parent JobServer object) and the name in the constructor.
        Dim op As [Operator]
        op = New [Operator](srv.JobServer, "Test_Operator")
        'Set the Net send address.
        op.NetSendAddress = "Network1_PC"
        'Create the operator on the instance of SQL Agent.
        op.Create()
        'Define a Job object variable by supplying the Agent and the name arguments in the constructor and setting properties.
        Dim jb As Job
        jb = New Job(srv.JobServer, "Test_Job")
        'Specify which operator to inform and the completion action.
        jb.OperatorToNetSend = "Test_Operator"
        jb.NetSendLevel = CompletionAction.Always
        'Create the job on the instance of SQL Agent. 
        jb.Create()
        'Define a JobStep object variable by supplying the parent job and name arguments in the constructor.
        Dim jbstp As JobStep
        jbstp = New JobStep(jb, "Test_Job_Step")
        jbstp.Command = "Test_StoredProc"
        jbstp.OnSuccessAction = StepCompletionAction.QuitWithSuccess
        jbstp.OnFailAction = StepCompletionAction.QuitWithFailure
        'Create the job step on the instance of SQL Agent.
        jbstp.Create()
        'Define a JobSchedule object variable by supplying the parent job and name arguments in the constructor. 
        Dim jbsch As JobSchedule
        jbsch = New JobSchedule(jb, "Test_Job_Schedule")
        'Set properties to define the schedule frequency, and duration.
        jbsch.FrequencyTypes = FrequencyTypes.Daily
        jbsch.FrequencySubDayTypes = FrequencySubDayTypes.Minute
        jbsch.FrequencySubDayInterval = 30
        Dim ts1 As TimeSpan
        ts1 = New TimeSpan(9, 0, 0)
        jbsch.ActiveStartTimeOfDay = ts1
        Dim ts2 As TimeSpan
        ts2 = New TimeSpan(17, 0, 0)
        jbsch.ActiveEndTimeOfDay = ts2
        jbsch.FrequencyInterval = 1
        Dim d As Date
        d = New Date(2003, 1, 1)
        jbsch.ActiveStartDate = d
        'Create the job schedule on the instance of SQL Agent.
        jbsch.Create()
        ' </snippetSMO_VBAgent1>



        Console.WriteLine("Job created successfully!")

        jbsch.Drop()
        jbstp.Drop()
        jb.Drop()
        op.Drop()

    End Sub

End Module
