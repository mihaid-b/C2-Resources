import socket

HOST = "0.0.0.0"
PORT = "443"

with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
    s.bind((HOST, PORT))
    s.listen()
    while True:
        print("Listening...")
        con, addr = s.accept()
        print(f"Connection received by {addr}")
        cmd = input("Enter command:")
        cmd = cmd + "\n"
        cdmReq = cmd.encode()
        con.sendall(cdmReq)
        cmdOut = con.recv(1024)
        print(cmdOut)
        