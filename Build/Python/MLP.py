import fileinput
import os
import webbrowser
import numpy as np
import glob
from sklearn.neural_network import MLPClassifier
from sklearn.linear_model import perceptron
from sklearn.model_selection import train_test_split
from sklearn.model_selection import KFold
from sklearn.metrics import confusion_matrix
import pickle
from sklearn.preprocessing import StandardScaler  
import time

#load file
data_full = np.loadtxt("G:/merged_partition_content/Project/Unity/TieuLuan01/DataTraning/balance.txt", delimiter = ',', dtype = int)
_input = data_full[0:304572,0:29]
_output = data_full[0:304572,29]


#print(_input)
#print(len(data_full))
#print(data_full[data_full[:30]])
#print(data_full(data_full[0,29] == 0))

 



def K_fold_MPL2(k):
    kf = KFold(n_splits=k)
    s = 0
    for train_index, test_index in kf.split(_input):
        X_train, X_test = _input[train_index], _input[test_index]
        Y_train, Y_test = _output[train_index], _output[test_index]
        scaler = StandardScaler()  
        scaler.fit(X_train)  
        X_train = scaler.transform(X_train)  
        X_test = scaler.transform(X_test)

        clf.fit(X_train, Y_train)
        #print(confusion_matrix(Y_test, clf.predict(X_test)))
        s += clf.score(X_test, Y_test)
    return 100*s/(k)
    



def K_fold_MPL(k):
    kf = KFold(n_splits=k)
    s = 0
    for train_index, test_index in kf.split(_input):
        X_train, X_test = _input[train_index], _input[test_index]
        Y_train, Y_test = _output[train_index], _output[test_index]
        clf.fit(X_train, Y_train)
        #print(confusion_matrix(Y_test, clf.predict(X_test)))
        s += clf.score(X_test, Y_test)
    return 100*s/(k)
    


def MegreFile():
    #file_list = {"Label_0.txt", "Label_1.txt"}
    file_list = os.listdir("G:/merged_partition_content/Project/Unity/TieuLuan01/DataTraning/Split/1")
    with open('full_balance.txt', 'w') as file:
        input_lines = fileinput.input(file_list)
        file.writelines(input_lines)
    
def SplitLable(filename_full):
    data_full = np.genfromtxt(filename_full, delimiter = ',', dtype = int)
    label_0 = data_full[data_full[:,29] == 0]
    label_1 = data_full[data_full[:,29] == 1]
    #maxRows = len(label_1)
    list = os.listdir("G:/merged_partition_content/Project/Unity/TieuLuan01/DataTraning/Split/0")
    number_files = len(list) + 1
    tr = 'G:/merged_partition_content/Project/Unity/TieuLuan01/DataTraning/Split/0/' + str(number_files)  + 'Label_0.txt'
    f = open(tr, 'w')
    c = 0
    for i in label_0:
        c = c+1
        re = []
        for j in range(0,30):
            re.append(i[j])
        f.write(str(re))
        f.write('\n')
        #if(c > maxRows):
            #break;
    f.close()
    list = os.listdir("G:/merged_partition_content/Project/Unity/TieuLuan01/DataTraning/Split/1")
    number_files = len(list) + 1
    tr = 'G:/merged_partition_content/Project/Unity/TieuLuan01/DataTraning/Split/1/' + str(number_files)  + 'Label_1.txt'
    f = open(tr, 'w')
    for i in label_1:
        re = []
        for j in range(0,30):
            re.append(i[j])
        f.write(str(re))
        f.write('\n')        
    f.close()

     
#modeling
p = perceptron.Perceptron(max_iter = 1000, eta0 = .01)
clf = MLPClassifier(solver='lbfgs', activation  = 'tanh',  alpha=1e-5, hidden_layer_sizes=(10,10), random_state=1)
if __name__ == "__main__":
    #SplitLable("G:/merged_partition_content/Project/Unity/TieuLuan01/DataTraning/181dataTraning.txt")
    #SplitLable("G:/merged_partition_content/Project/Unity/TieuLuan01/DataTraning/full_balance.txt")
    #MegreFile()
    #MegreFile("G:/merged_partition_content/Project/Unity/TieuLuan01/DataTraning/Split/0")
    #print(clf)
    #print(K_fold_MPL2(10))
    start = time.time()
    #save model
    print(K_fold_MPL(10))
    pickle.dump(clf, open("model1010", 'wb'))
    print(time.time() - start)
    os.system("G:/merged_partition_content/Project/Unity/TieuLuan01/Python/gcmt.mp3")

    '''t = os.listdir("G:/merged_partition_content/Project/Unity/TieuLuan01/DataTraning")
    for i in t:
        tr = "G:/merged_partition_content/Project/Unity/TieuLuan01/DataTraning/" + i
        print(tr)
        SplitLable(tr)'''
        
        
    #print(os.listdir("G:/merged_partition_content/Project/Unity/TieuLuan01/DataTraning"))
    
    

