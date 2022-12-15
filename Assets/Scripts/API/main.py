from flask import Flask, request
from flask_restful import Api,Resource
from fuzzylogic import *

app = Flask(__name__)
api = Api(app)

class FuzzyAPI(Resource):
    #  [0] AngerXFear [Range(-1f, 1f)]
    #  [1] DisgustXTrust [Range(-1f, 1f)]
    #  [2] SadnessXJoy [Range(-1f, 1f)]
    #  [3] AntecipationXSurprise [Range(-1f, 1f)]
    
    def get(self):
        emo = getEmotion();
        return {"emotion" : emo}

    def post(self):
        emo = []
        emo.append(request.form['axeAF'])
        emo.append(request.form['axeDT'])
        emo.append(request.form['axeSJ'])
        emo.append(request.form['axeAS'])
        for i in range(4):
            emo[i] = emo[i].replace(",",".");
            emo[i] = float(emo[i]);
        postEmotion(emo);
        return;
        # return request.json();

api.add_resource(FuzzyAPI, "/fuzzyemotionapi")
# /<float:axeAF>/<float:axeDT>/<float:axeSJ>/<float:axeAS>



if __name__ == "__main__":
    app.run(debug=True) # do not set debug = true for production
