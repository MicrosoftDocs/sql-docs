Imports Microsoft.SqlServer.Management.Smo
Imports Microsoft.SqlServer.Management.Common
Module SMO_VBEvents1
    ' <snippetSMO_VBEvents1>
    'Create an event handler subroutine that runs when a table is created.
    Private Sub MyCreateEventHandler(ByVal sender As Object, ByVal e As ServerEventArgs)
        Console.WriteLine("A table has just been added to the AdventureWorks2012 2008 database.")
    End Sub
    'Create an event handler subroutine that runs when a table is deleted.
    Private Sub MyDropEventHandler(ByVal sender As Object, ByVal e As ServerEventArgs)
        Console.WriteLine("A table has just been dropped from the AdventureWorks2012 2008 database.")
    End Sub
    Sub Main()
        'Connect to the local, default instance of SQL Server.
        Dim srv As Server
        srv = New Server
        'Reference the AdventureWorks2012 database.
        Dim db As Database
        db = srv.Databases("AdventureWorks2012")
        'Create a database event set that contains the CreateTable event only.
        Dim databaseCreateEventSet As New DatabaseEventSet
        databaseCreateEventSet.CreateTable = True
        'Create a server event handler and set it to the first event handler subroutine.
        Dim serverCreateEventHandler As ServerEventHandler
        serverCreateEventHandler = New ServerEventHandler(AddressOf MyCreateEventHandler)
        'Subscribe to the first server event handler when a CreateTable event occurs.
        db.Events.SubscribeToEvents(databaseCreateEventSet, serverCreateEventHandler)
        'Create a database event set that contains the DropTable event only.
        Dim databaseDropEventSet As New DatabaseEventSet
        databaseDropEventSet.DropTable = True
        'Create a server event handler and set it to the second event handler subroutine.
        Dim serverDropEventHandler As ServerEventHandler
        serverDropEventHandler = New ServerEventHandler(AddressOf MyDropEventHandler)
        'Subscribe to the second server event handler when a DropTable event occurs.
        db.Events.SubscribeToEvents(databaseDropEventSet, serverDropEventHandler)
        'Start event handling.
        db.Events.StartEvents()
        'Create a table on the database.
        Dim tb As Table
        tb = New Table(db, "Test_Table")
        Dim mycol1 As Column
        mycol1 = New Column(tb, "Name", DataType.NChar(50))
        mycol1.Collation = "Latin1_General_CI_AS"
        mycol1.Nullable = True
        tb.Columns.Add(mycol1)
        tb.Create()
        'Remove the table.
        tb.Drop()
        'Wait until the events have occured.
        Dim x As Integer
        Dim y As Integer
        For x = 1 To 1000000000
            y = x * 2
        Next
        'Stop event handling.
        db.Events.StopEvents()





    End Sub
    ' </snippetSMO_VBEvents1>


End Module
