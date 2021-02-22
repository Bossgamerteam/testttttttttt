/*
    Classes
*/

class MapAsset {
    constructor(name, type, unityType, x, y, z, xScale, yScale, zRotate)
    {
        this.name = name;
        this.type = type;
        this.unityType = unityType;
        this.x = x;
        this.y = y;
        this.z = z;
        this.xScale = xScale;
        this.yScale = yScale;
        this.zRotate = zRotate;
        this.colliders = [];

        if (type == "custom") {
            this.height = custom[custom.length - 1].height;
            this.width = custom[custom.length - 1].width;
        } else {
            this.height = assets[type].height;
            this.width = assets[type].width;
        }
        
    }
}
class Asset {
    constructor(name)
    {
        this.name = name;
        this.sprite = {};
        this.width = 0;
        this.height = 0;
    }
}
class Collider {
    constructor(asset)
    {
        this.points = [{x: asset.x, y:-asset.y}, {x: asset.x + 1, y: -asset.y + 1}];
    }
}

/*
    Asset Dictionary
*/

var assets = {
    "snowman":"Snowman",
    "dead-snowman":"Dead Snowman",
    "rock1":"Rock 1",
    "rock2":"Rock 2",
    "rock3":"Rock 3",
    "rock4":"Rock 4",
    "rock5":"Rock 5",
    "polus-admin":"Polus Admin",
    "polus-comms":"Polus Comms",
    "polus-electric":"Polus Electric",
    "polus-medbay":"Polus Medbay",
    "polus-o2":"Polus O2",
    "polus-specimen":"Polus Specimen",
    "polus-specimen-hallway-1":"Polus Specimen Hall 1",
    "polus-specimen-hallway-2":"Polus Specimen Hall 2",
    "polus-storage":"Polus Storage",
    "polus-weapons":"Polus Weapons",
    "skeld-admin-hallway":"Skeld Admin Hall",
    "skeld-cafe":"Skeld Cafe",
    "skeld-electric":"Skeld Electric",
    "skeld-engine-1":"Skeld Engine 1",
    "skeld-engine-2":"Skeld Engine 2",
    "skeld-medbay":"Skeld Medbay",
    "skeld-nav-hallway":"Skeld Nav Hall",
    "skeld-o2":"Skeld O2",
    "skeld-reactor-hallway":"Skeld Reactor Hall",
    "skeld-security":"Skeld Security",
    "skeld-storage":"Skeld Storage",
    "sab-reactor-left":"Left Reactor [Sabatage]",
    "sab-reactor-right":"Right Reactor [Sabatage]",
    "sab-electric":"Lights Panel [Sabatage]",
    "task-telescope":"Telescope [Task]",
    "task-chart":"Chart Course [Task]",
    "task-oxygen":"Fill O2 Canisters [Task]",
    "task-tree":"Tree Monitor [Task]",
    "task-temp1":"Record Temp 1 [Task]",
    "task-temp2":"Record Temp 2 [Task]",
    "task-artifacts":"Store Artifacts [Task]",
    "task-simonsays":"Simon Says [Task]",
    "task-counting":"Unlock Manifolds [Task]",
    "task-medscan":"Medscan [Task]",
    "task-keys":"Keys [Task]",
    "task-drill":"Drill [Task]",
    "task-id":"Swipe Card [Task]",
    "task-pass":"Scan Pass [Task]",
    "task-node1":"Node 1 [Task]",
    "task-node2":"Node 2 [Task]",
    "task-node3":"Node 3 [Task]",
    "task-node4":"Node 4 [Task]",
    "task-node5":"Node 5 [Task]",
    "task-node6":"Node 6 [Task]",
    "task-samples":"Inspect Samples [Task]",
    "task-waterwheel1":"Water Wheel 1 [Task]",
    "task-waterwheel2":"Water Wheel 2 [Task]",
    "task-waterwheel3":"Water Wheel 3 [Task]",
    "task-router":"Reboot Router [Task]",
    "task-garbage":"Empty Garbage [Task]",
    "task-weapons":"Weapons [Task]",
    "task-waterjug1":"Replace Water Jug 1 [Task]",
    "task-waterjug2":"Replace Water Jug 2 [Task]",
    "task-download1":"Download 1 [Task]",
    "task-download2":"Download 2 [Task]",
    "task-download3":"Download 3 [Task]",
    "task-download4":"Download 4 [Task]",
    "task-download5":"Download 5 [Task]",
    "task-upload":"Upload [Task]",
    "task-wires1":"Wires 1 [Task]",
    "task-wires2":"Wires 2 [Task]",
    "task-wires3":"Wires 3 [Task]",
    "task-wires4":"Wires 4 [Task]",
    "task-wires5":"Wires 5 [Task]",
    "task-wires6":"Wires 6 [Task]",
    "task-nodeswitch":"Node Switches [Task]",
    "util-button":"Emergency Button [Util]",
    "util-admin":"Admin Table [Util]",
    "util-vitals":"Vitals [Util]",
    "util-cams":"Cameras [Util]",
    "util-computer":"Computer [Util]"
};

var custom = [];

/*
    Modules
*/

let map = {
    data: [],

    add: function(type) {
        var unityType = type.includes("util-") || type.includes("task-") || type.includes("sab-") ? 1 : 213;
        if (type == "custom")
            unityType = 0;
        this.data.push(new MapAsset("New Asset", type, unityType, 0, 0, 0, 1, 1, 0));
        properties.updateAssetList(type, true);
        selection.set(this.data.length - 1);
        selection.isNew = true;
        properties.hideAdd();
    },

    download: function() {

        // Make Colliders Relative
        for (var i = 0; i < map.data.length; i++)
        {
            for (var o = 0; o < map.data[i].colliders.length; o++)
            {
                for (var p = 0; p < map.data[i].colliders[o].points.length; p++)
                {
                    var point = map.data[i].colliders[o].points[p];
                    point.rx = point.x - map.data[i].x;
                    point.ry = point.y + map.data[i].y;
                }
            }
        }

        var outputData = {
            name: document.getElementById("mapName").value,
            assets: map.data
        };
        var zip = new JSZip();
        zip.file("properties.json", JSON.stringify(outputData));
        zip.generateAsync({type:"blob"})
        .then(function(content) {
            saveAs(content, "map.zip");
        });
    },

    checkFile: function(event) {
        if (event.target.files[0]) {
            document.getElementById("addButton").disabled = false;
        } else {
            document.getElementById("addButton").disabled = true;
        }
    },

    addCustom: function() {
        var file = document.getElementById("customFile").files[0];

        var fr = new FileReader();
        fr.onload = function () {
            loadImage(fr.result, img => {
                custom.push(img);
                map.add("custom");
                map.data[map.data.length - 1].curl = fr.result;
                map.data[map.data.length - 1].customIndex = custom.length - 1;
            });

            properties.hideUpload();
        }
        fr.readAsDataURL(file);

        if (custom.length > 100)
        {
            document.getElementById("warning").style.display = "revert";
            document.getElementById("controls5").style.height = "250px";
        }
        
    }
};

let collisions = {

    editingIndex: -1,
    dragIndex: -1,
    closestPoint: {x:0, y:0},
    closestIndex: -1,

    add: function() {
        if (selection.index == -1)
            return;
        selection.get().colliders.push(new Collider(selection.get()));
        this.updateHTML();
    },

    updateHTML: function() {
        document.getElementById("colliders").innerHTML = "";
        if (selection.index != -1)
        {
            var sel = selection.get();
            for (var i = 0; i < sel.colliders.length; i++)
            {
                var str = "<li id=\"col-" + i + "\"class=\"list-group-item\">" + (i + 1) + "<button type=\"button\" class=\"btn btn-danger btn-edit\" onclick=\"collisions.remove(" + i + ");\">-</button><button type=\"button\" class=\"btn btn-warning btn-edit\" onclick=\"collisions.edit(" + i + ")\">Edit</button></li>";
                document.getElementById("colliders").innerHTML += str;
            }
        }
    },

    update: function() {
        if (mouseIsPressed && (mouseButton === LEFT || mouseButton === RIGHT) && !properties.windowOpen && mouseX < window.innerWidth - 300 && mouseY > 0 && this.dragIndex == -1 && this.editingIndex != -1)
        {
            var colliders = this.get();
            
            for (var i = 0; i < colliders.points.length; i++)
            {
                var p = colliders.points[i];
                var dst = Math.sqrt(Math.pow(graphics.trueMouseX - p.x, 2) + Math.pow(-graphics.trueMouseY - p.y, 2));
                if (dst < .2) {
                    if (mouseButton === LEFT) {
                        this.dragIndex = i;
                        this.closestPoint = p
                    } else {
                        colliders.points.splice(i, 1);
                    }
                }
            }

            if (this.dragIndex == -1 && this.closestIndex != -1 && mouseButton === LEFT)
            {
                colliders.points.splice(this.closestIndex + 1, 0, {x: graphics.trueMouseX, y: -graphics.trueMouseY});
                this.dragIndex = this.closestIndex + 1;
            }
        }
        else if (!mouseIsPressed)
        {
            this.dragIndex = -1;

            this.closestIndex = -1;
            var shortestLineDist = Number.MAX_SAFE_INTEGER;
            if (this.editingIndex != -1)
            {
                var colliders = this.get();

                for (var i = 0; i < colliders.points.length - 1; i++)
                {
                    var p1 = colliders.points[i];
                    var p2 = colliders.points[i+1];

                    if (p1.x > p2.x)
                    {
                        var t = p1;
                        p1 = p2;
                        p2 = t;
                    }

                    var slope = (p1.y - p2.y) / (p1.x - p2.x);
                    var intercept = p1.y - (slope * p1.x);

                    var p3 = {x:0, y:0};
                    p3.x = (graphics.trueMouseX + (slope * -graphics.trueMouseY) - (slope * intercept)) / ((slope * slope) + 1);
                    p3.y = (slope * p3.x) + intercept;

                    if (p3.x < p1.x)
                    {
                        p3 = p1;
                    }

                    if (p3.x > p2.x)
                    {
                        p3 = p2;
                    }

                    var d = Math.sqrt(Math.pow(graphics.trueMouseX - p3.x, 2) + Math.pow(-graphics.trueMouseY - p3.y, 2));

                    if (d < shortestLineDist && d < .5)
                    {
                        shortestLineDist = d;
                        this.closestPoint = p3;
                        this.closestIndex = i;
                    }
                }
            }
        }

        if (this.dragIndex != -1)
        {
            var c = this.get();
            c.points[this.dragIndex].x = graphics.trueMouseX;
            c.points[this.dragIndex].y = -graphics.trueMouseY;
        }
    },

    remove: function(index) {
        if (selection.index == -1)
            return;
        selection.get().colliders.splice(index, 1);
        this.editingIndex = -1;
        this.updateHTML();
    },

    edit: function(index) {
        if (selection.index == -1)
            return;
        if (this.editingIndex == index)
            this.editingIndex = -1;
        else
            this.editingIndex = index;
    },

    get: function() {
        if (selection.index == -1 || this.editingIndex == -1)
            return;
        return selection.get().colliders[this.editingIndex];
    }

};

let selection = {
    index: -1,
    isNew: false,
    isDrag: false,
    dragX: 0,
    dragY: 0,

    set: function(index) {
        if (!index in map.data)
            return;
        this.index = index;
        collisions.editingIndex = -1;
        properties.get();
    },

    unset: function() {
        this.index = -1;
        this.isNew = false;
        collisions.editingIndex = -1;
        properties.clear();
    },

    delete: function() {
        if (this.index == -1)
            return;
        properties.updateAssetList(map.data[this.index].type, false);
        map.data.splice(this.index, 1);
        this.isNew = false;
        this.index = -1;
        collisions.editingIndex = -1;
    },

    get: function() {
        if (this.index == -1)
            return undefined;
        return map.data[this.index];
    },

    update: function() {
        if (this.isNew && this.index != -1) {
            this.get().x = graphics.trueMouseX;
            this.get().y = graphics.trueMouseY;
            properties.get();
            document.body.style.cursor = "move";
            if (mouseIsPressed) {
                this.isNew = false;
                document.body.style.cursor = "default";
            }
        } else if (mouseIsPressed && !properties.windowOpen && mouseButton === LEFT && mouseX < window.innerWidth - 300 && mouseY > 0 && !this.isDrag && collisions.dragIndex == -1) {
            var newIndex = -1;
            for (var i = 0; i < map.data.length; i++)
            {
                var obj = map.data[i];
                var w = (obj.width * obj.xScale)/graphics.camera.scale;
                var h = (obj.height * obj.yScale)/graphics.camera.scale;

                if (graphics.trueMouseX > obj.x - (w/2) &&
                    graphics.trueMouseX < obj.x + (w/2) &&
                    graphics.trueMouseY < obj.y + (h/2) &&
                    graphics.trueMouseY > obj.y - (h/2)) {
                    newIndex = i;
                }
            }
            if (newIndex == -1)
            {
                this.unset();
            }
            else
            {
                this.set(newIndex);
                this.isDrag = true;
                this.dragX = graphics.trueMouseX - this.get().x;
                this.dragY = graphics.trueMouseY - this.get().y;
                document.body.style.cursor = "move";
            }
        }
        else if (!mouseIsPressed)
        {
            this.isDrag = false;
            document.body.style.cursor = "default";
        }

        if (this.isDrag)
        {
            this.get().x =  graphics.trueMouseX - this.dragX;
            this.get().y =  graphics.trueMouseY - this.dragY;
            properties.get();
        }
    }
};

let properties = {
    windowOpen: false,

    init: function() {
        for(var key in assets) {
            var cssClass = "";
            if (assets[key].name.includes("[Sabatage]"))
                cssClass = " sabatage";
            if (assets[key].name.includes("[Task]"))
                cssClass = " task";
            if (assets[key].name.includes("[Util]"))
                cssClass = " util";

            document.getElementById("add-assets").innerHTML += "<button type=\"button\" id=\"add-" + key + "\"class=\"list-group-item list-group-item-action" + cssClass + "\" onclick=\"map.add('" + key + "');\">" + assets[key].name + "</button>";
            if (cssClass == " task" || key == "util-button")
                document.getElementById("req-assets").innerHTML += "<button type=\"button\" id=\"req-" + key + "\"class=\"list-group-item list-group-item-action\" onclick=\"map.add('" + key + "');\">" + assets[key].name + "</button>";
        }

        map.data.forEach(asset => {
            if (asset.type == "custom")
                return;
            if (assets[asset.type].name.includes("[")) {
                document.getElementById("add-" + asset.type).style.display = "none";
                document.getElementById("req-" + asset.type).style.display = "none";
            }
        });
    },

    updateAssetList(type, isAdded)
    {
        if (type == "custom")
            return;
        if (!assets[type].name.includes("["))
            return;
        if (isAdded)
        {
            document.getElementById("add-" + type).style.display = "none";
            document.getElementById("req-" + type).style.display = "none";
        } else {
            document.getElementById("add-" + type).style.display = "revert";
            document.getElementById("req-" + type).style.display = "revert";
        }
    },

    showAdd: function() {
        document.getElementById("controls3").style.display = "revert";
        this.windowOpen = true;
    },

    hideAdd: function() {
        document.getElementById("controls3").style.display = "none";
        this.windowOpen = false;
    },

    showDownload: function() {
        document.getElementById("controls4").style.display = "revert";
        this.windowOpen = true;
    },

    hideDownload: function() {
        document.getElementById("controls4").style.display = "none";
        this.windowOpen = false;
    },

    showUpload: function() {
        document.getElementById("controls5").style.display = "revert";
        this.windowOpen = true;
    },

    hideUpload: function() {
        document.getElementById("controls5").style.display = "none";
        this.windowOpen = false;
    },

    set: function() {
        if (selection.index == -1)
            return;
        
        selection.get().name     = document.getElementById("prop1").value;
        selection.get().x        = parseFloat(document.getElementById("prop2").value);
        selection.get().y        = parseFloat(document.getElementById("prop3").value);
        selection.get().z        = parseFloat(document.getElementById("prop4").value);
        selection.get().xScale   = parseFloat(document.getElementById("prop5").value);
        selection.get().yScale   = parseFloat(document.getElementById("prop6").value);
        selection.get().zRotate  = parseFloat(document.getElementById("prop7").value);
    },

    get: function() {
        if (selection.index == -1)
            return;

        var s = selection.get();
        var asset = assets[s.type];
        if (asset)
            document.getElementById("prop0").innerText = asset.name;
        else
            document.getElementById("prop0").innerText = "Custom Asset";
        document.getElementById("prop1").value = s.name;
        document.getElementById("prop3").value = s.y;
        document.getElementById("prop2").value = s.x;
        document.getElementById("prop4").value = s.z;
        document.getElementById("prop5").value = s.xScale;
        document.getElementById("prop6").value = s.yScale;
        document.getElementById("prop7").value = s.zRotate;

        collisions.updateHTML();
    },

    clear: function() {
        document.getElementById("prop0").innerText = "Properties";
        for (var i = 1; i <= 7; i++)
            document.getElementById("prop" + i).value = "";
        collisions.updateHTML();
    },
};

let graphics = {
    camera: {
        isDrag: false,
        gridFactor: 1,
        dragX: 0,
        dragY: 0,
        x: 0,
        y: 0,
        scale: 100,
        zoom: 100,
        zoomDelta: 1.1
    },

    trueMouseX:0,
    trueMouseY:0,

    init: function() {
        createCanvas(window.innerWidth - 300, window.innerHeight - 60);
        for (var key in assets) {
            assets[key] = new Asset(assets[key]);

            let imageLocation = "sprites/" + key + ".png";
            let k = key;
            assets[key].sprite = loadImage(imageLocation, img => {
                assets[k].width = img.width;
                assets[k].height = img.height;
            });
        }
    },

    resize: function() {
        resizeCanvas(window.innerWidth - 300, window.innerHeight - 60);
    },

    drawAsset: function(asset) {
        try {
            var img = undefined;
            if (asset.type == "custom")
                img = custom[asset.customIndex];
            else
                img = assets[asset.type].sprite;

            var w = (asset.width * asset.xScale) / this.camera.scale;
            var h = (asset.height * asset.yScale) / this.camera.scale;
            image(img, asset.x - (w/2) - this.camera.x, -(asset.y + (h/2) - this.camera.y), w, h);
        } catch {}
    },

    drawMap: function() {
        for (var i = 0; i < map.data.length; i++)
        {
            var leastZ = Number.MIN_SAFE_INTEGER;
            var leastIndex = -1;
            for (var o = i; o < map.data.length; o++)
            {
                if (map.data[o].z > leastZ)
                {
                    leastZ = map.data[o].z;
                    leastIndex = o;
                }
            }

            var temp = map.data[i];
            map.data[i] = map.data[leastIndex];
            map.data[leastIndex] = temp;

            if (selection.index == i)
                selection.index = leastIndex;
            else if (selection.index == leastIndex)
            selection.index = i;

            this.drawAsset(temp);
        }
    },

    drawSelection: function() {
        if (selection.index == -1)
            return;
        var selectedAsset = selection.get();
        noFill();
        stroke(255,255,255, 245);
        var w = (selectedAsset.width * selectedAsset.xScale) / this.camera.scale;
        var h = (selectedAsset.height * selectedAsset.yScale) / this.camera.scale;
        rect(selectedAsset.x - (w/2) - this.camera.x, -(selectedAsset.y + (h/2) - this.camera.y), w, h);
    },

    drawGrid: function() {
        strokeWeight(1/this.camera.scale);
        for (var i = -200; i < 200; i++) {
            if (i % 10 == 0)
                stroke(90,90,90);
            else
                stroke(40,40,40);
            line(Number.MIN_SAFE_INTEGER, -((i*this.camera.gridFactor)-this.camera.y), Number.MAX_SAFE_INTEGER, -((i*this.camera.gridFactor)-this.camera.y));
            line(((i*this.camera.gridFactor)-this.camera.x), Number.MIN_SAFE_INTEGER, ((i*this.camera.gridFactor)-this.camera.x), Number.MAX_SAFE_INTEGER);
        }
    },

    drawSpawn: function() {
        noFill();
        strokeWeight(1/this.camera.scale);
        stroke(255,255,255,255);
        circle((16.5-this.camera.x), -(-1-this.camera.y), 3);
        circle((19.5-this.camera.x), -(-17-this.camera.y), 3);
        stroke("black");
        textSize(.3);
        textFont('Bahnschrift');
        fill("white");
        text('Default Spawn', (16.5-this.camera.x) - (textWidth("Default Spawn")/2), -(-1-this.camera.y));
        text('Button Spawn', (19.5-this.camera.x) - (textWidth("Button Spawn")/2), -(-17-this.camera.y));
    },

    drawColliders: function() {
        if (selection.index == -1 || collisions.editingIndex == -1)
            return;
        var colliders = collisions.get();
        stroke(107, 173, 33,255);
        strokeWeight(4/this.camera.scale);
        fill(107, 173, 33,255);
        for (var i = 0; i < colliders.points.length - 1; i++)
        {
            var p = colliders.points[i];
            var p2 = colliders.points[i+1];
            line(p.x - this.camera.x, p.y + this.camera.y, p2.x - this.camera.x, p2.y + this.camera.y);
        }
        if (collisions.closestIndex != -1)
            circle(collisions.closestPoint.x - this.camera.x, collisions.closestPoint.y + this.camera.y, 10/this.camera.scale);
    },

    adjustZoom: function(delta) {
        if (properties.windowOpen)
            return;
        if (delta > 0)
            this.camera.zoom /= this.camera.zoomDelta;
        else
            this.camera.zoom *= this.camera.zoomDelta;
    },

    update: function() {
        this.trueMouseX = ((mouseX - (width / 2)) / (this.camera.zoom))+this.camera.x;
        this.trueMouseY = -((mouseY - (height / 2)) / (this.camera.zoom))+this.camera.y;

        if (mouseIsPressed && mouseButton === CENTER && mouseX < window.innerWidth - 300 && mouseY > 0 && !this.camera.isDrag)
        {
            this.camera.isDrag = true;
            this.camera.dragX = this.trueMouseX;
            this.camera.dragY = this.trueMouseY;
            document.body.style.cursor = "move";
        }
        else if (!mouseIsPressed)
        {
            document.body.style.cursor = "default";
            this.camera.isDrag = false;
        }

        if (this.camera.isDrag)
        {
            this.camera.x += this.camera.dragX - this.trueMouseX;
            this.camera.y += this.camera.dragY - this.trueMouseY;
        }

        //document.getElementById("debug").innerText = (Math.round(this.trueMouseX*10)/10) + "," + (Math.round(this.trueMouseY*10)/10);

        translate(width/2,height/2);
        scale(this.camera.zoom);
    }
};

/*
    p5.js
*/

function setup() {
    graphics.init();

    // Map Storage
    var loadedMap = localStorage.getItem("map");
    if (loadedMap) {
        map.data = JSON.parse(loadedMap);

        map.data.forEach(asset => {
            if (asset.type == "custom")
            {
                map.data.customIndex = custom.length;
                custom.push(loadImage(asset.curl));
            };
        });
    }

    if (custom.length > 100)
    {
        document.getElementById("warning").style.display = "revert";
        document.getElementById("controls5").style.height = "250px";
    }

    properties.init();

    window.onbeforeunload = function () {
        localStorage.setItem("map", JSON.stringify(map.data));
    };
    document.oncontextmenu = function() {
        return false;
    };

    // Mobile
    if (/Android|webOS|iPhone|iPad|iPod|BlackBerry|IEMobile|Opera Mini/i.test(navigator.userAgent))
        alert("The editor doesn't support mobile devices...but I'm not gonna stop you from trying.")
}
function windowResized() {
    graphics.resize();
}
function draw() {
    background("black");

    graphics.update();

    graphics.drawGrid();
    graphics.drawMap();
    graphics.drawSpawn();
    graphics.drawSelection();
    graphics.drawColliders();
    
    collisions.update();
    selection.update();
    
}
function mouseWheel(e)
{
    graphics.adjustZoom(e.delta);
}