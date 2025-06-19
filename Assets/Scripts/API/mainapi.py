from flask import Flask, request, jsonify
# from flask_restful import Api,Resource
from llama_cpp import Llama
from fuzzylogic import *

app = Flask(__name__)
# api = Api(app)
llm = Llama(model_path="models/llama-pro-8b-instruct.Q6_K.gguf", n_ctx=4096)

# class FuzzyAPI(Resource):

#  [0] AngerXFear [Range(-1f, 1f)]
#  [1] DisgustXTrust [Range(-1f, 1f)]
#  [2] SadnessXJoy [Range(-1f, 1f)]
#  [3] AntecipationXSurprise [Range(-1f, 1f)]
@app.route('/fuzzyapi', methods=['GET'])
def get():
    emo = getEmotion()
    return {"emotion" : emo}

@app.route('/fuzzyapi', methods=['POST'])
def post():
    emo = []
    emo.append(request.form['axeAF'])
    emo.append(request.form['axeDT'])
    emo.append(request.form['axeSJ'])
    emo.append(request.form['axeAS'])
    for i in range(4):
        emo[i] = emo[i].replace(",",".")
        emo[i] = float(emo[i])
    postEmotion(emo)
    return '', 204 
    # return request.json();
# /<float:axeAF>/<float:axeDT>/<float:axeSJ>/<float:axeAS>

# class LlamaAPI(Resource):
@app.route('/llamaapi', methods=['POST'])
def generate():
    data = request.get_json()
    prompt = data.get("prompt", "")
    max_tokens = data.get("max_tokens", 30)
    
    output = llm(prompt, max_tokens=max_tokens)
    return jsonify(output)
    

# api.add_resource(FuzzyAPI, "/fuzzyemotionapi")
# /<float:axeAF>/<float:axeDT>/<float:axeSJ>/<float:axeAS>

# api.add_resource(LlamaAPI, "/llamaapi")


# /<float:axeAF>/<float:axeDT>/<float:axeSJ>/<float:axeAS>



if __name__ == "__main__":
    app.run(port=11434)
