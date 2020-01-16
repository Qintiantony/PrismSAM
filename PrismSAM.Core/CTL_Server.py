import sys
import socket
import os
import numpy
from toptica.lasersdk.dlcpro.v1_7_0 import DLCpro, NetworkConnection

class CTL_Server:
    def __init__(self, host = ("", 8688), CTL_addr=None):
        self.host = host
        self.CTL_addr = str(CTL_addr)
        self.setSocket(self.host)
        self.serverExit = False
    def setSocket(self, host):
        try:
            self.socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            self.socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR,1)
            self.socket.bind(self.host)
            self.socket.listen(5)
            print("CTL Server listening at port: %d" %self.host[1])
        except:
            print("Fail to start CTL server!")
            
    def process_command(self, client, addr):
        try:
            while(not self.serverExit):
                command = client.recv(8).decode("utf-8")
                print("Command received: ", command)
                with DLCpro(NetworkConnection(self.CTL_addr)) as dlcpro:
                    if (command == "QUITSERV"):
                        self.serverExit=True
                        feedback = "CTL Disconnected"
                    elif (command == "CTLSTATE"):
                        feedback = str(dlcpro.laser1.ctl.state.get())
                    else:
                        ctl_status = str(dlcpro.laser1.ctl.state.get())
                        if (command == "SC_START" and ctl_status=="0"):
                            dlcpro.laser1.ctl.scan.start()
                            feedback = "CTL scan started"
                        elif (command == "SC__STOP" and ctl_status=="3"):
                            dlcpro.laser1.ctl.scan.stop()
                            feedback = "CTL scan stopped"
                        elif (command == "SC_PAUSE" and ctl_status=="3"):
                            dlcpro.laser1.ctl.scan.pause()
                            feedback = "CTL scan paused"
                        elif (command == "SC_CONTI" and ctl_status=="5"):
                            dlcpro.laser1.ctl.scan.continue_()
                            feedback = "CTL scan continued"
                        elif (command == "CTLWAVLE"):
                            feedback = str(dlcpro.laser1.ctl.wavelength_act.get())
                client.send(feedback.encode("utf-8"))
        except:
            feedback = "Connection error, please check CTL IP!"
            client.send(feedback.encode("utf-8"))
        
    def run(self):
        client, addr = self.socket.accept()
        print("Client: ", client)
        print("Address: ", addr)
        self.serverExit=False
        self.process_command(client, addr)

if __name__ == "__main__":
    CTL_IP = sys.argv[1]
    server = CTL_Server(CTL_addr=CTL_IP)
    server.run()
