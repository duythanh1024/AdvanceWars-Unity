from sklearn.neural_network import MLPClassifier
import pickle
import numpy as np
import socket
import time
from random import randint
HOST = "localhost"
PORT = 5454
PORTS = 5455

waitFeedback = False
sentDecision = False
dec = "none"

def Process(_input):
    t0 = time.clock()
    t = np.fromstring(_input, dtype=int, sep=',')
    t = t.reshape(1, -1)
    #print("t = ", t)
    resl = clf.predict(t)
    #print("time = ", time.clock() - t0)
    print("predict ", resl)
    if(resl == 1):
        return "OK"
    else:
        return "No"
    '''r = randint(0, 1)
    if(r == 1):
        return "OK"
    else:
        return "not"'''
    
    

def Server():
    ss = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    s.bind((HOST, PORT))
    s.settimeout(1)
    while 1:
        try:
            rec = s.recv(1024)
            print(rec)
            if (len(rec) > 10):
                dec = Process(rec)
                #dec = "OK";
                sentDecision = True
            else:
                #print("feedack from unity")
                sentDecision = False
                
            if (sentDecision):
                #time.sleep(3)
                ss.sendto(dec.encode(), (HOST,PORTS))
                #print("sending...", dec)
                #time.sleep(1)           
                
        except:
            i =0
            print("runing")
            

clf = pickle.load(open('model1', 'rb'))    
if __name__ == "__main__":
    Server()


    
