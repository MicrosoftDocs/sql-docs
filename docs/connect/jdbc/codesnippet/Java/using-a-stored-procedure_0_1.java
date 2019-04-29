public static void executeUpdateStoredProcedure(Connection con) {
    try(CallableStatement cstmt = con.prepareCall("{call dbo.UpdateTestTable(?, ?)}");) {
        cstmt.setString(1, "A");
        cstmt.setInt(2, 100);
        cstmt.execute();
        int count = cstmt.getUpdateCount();
        System.out.println("ROWS AFFECTED: " + count);
    }
    // Handle any errors that may have occurred.
    catch (SQLException e) {
        e.printStackTrace();
    }
}
