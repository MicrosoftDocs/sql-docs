try(CallableStatement cstmt = con.prepareCall("{call employee_jobid(?)}");) {
    String lname = "Brown";
    cstmt.setString(1, lname);
    ResultSet rs = cstmt.executeQuery();
}
