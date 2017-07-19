var cy = null;
var response = null;
var best = null;
var all = null;
var count;

var url = "http://localhost:52446/api/pathes/";
var request = new XMLHttpRequest();
request.open("GET", url);
request.onreadystatechange = function () {
    if (request.readyState === 4 && request.status === 200) {
        response = JSON.parse(request.responseText);
        draw();
    }
};
request.send();

function draw() {
    var _nodes = new Array(response.vertexes.length);
    for (var i = 0; i < _nodes.length; i++) {
        _nodes[i] = {
            data: { id: response.vertexes[i] }
        };
    }
    var _edges = new Array(response.edges.length);
    for (var j = 0; j < _edges.length; j++) {
        _edges[j] = {
            data: {
                id: response.edges[j].id,
                source: response.edges[j].source,
                target: response.edges[j].target,
                weight: response.edges[j].weight
            }
        };
    }

    cy = cytoscape({
        container: document.getElementById('cy'),
        boxSelectionEnabled: false,
        autounselectify: true,

        style: cytoscape.stylesheet()
            .selector('node')
            .css({
                'content': 'data(id)'
            })
            .selector('edge')
            .css({
                'curve-style': 'bezier',
                'target-arrow-shape': 'triangle',
                'width': 4,
                'line-color': '#ddd',
                'target-arrow-color': '#ddd',
                'label': 'data(weight)'
            }),

        elements: {
            nodes: _nodes,
            edges: _edges
        },
        layout: {
            name: 'circle',
            directed: true
        }
    });
}

function findBest () {
    var xhr = new XMLHttpRequest();

    var body =
        {
            graph: new Array(response.edges.length),
            starting: document.getElementById('starting').value,
            final: document.getElementById('final').value
        };
    for (var i = 0; i < body.graph.length; i++) {
        body.graph[i] = {
            name: response.edges[i].id,
            start: {
                key: response.edges[i].source
            },
            finish: {
                key: response.edges[i].target
            },
            weight: response.edges[i].weight
        };
    }

    xhr.open("POST", "http://localhost:52446/api/pathes/best/");
    xhr.setRequestHeader('Content-Type', 'application/json');

    xhr.onreadystatechange = function () {
        if (request.readyState === 4 && request.status === 200) {
            best = JSON.parse(xhr.responseText)[0];
            showBest();
        }
    };

    xhr.send(JSON.stringify(body));
}

function showBest () {
    cy.edges().animate({
        style: { lineColor: '#ddd' }
    });
    
    for (var i = 0; i < best.length; i++) {
        var param = "[id = " + "'" + best[i].name + "'" + "]";
        cy.edges(param).animate({
            style: { lineColor: 'blue' }
        });
    }
}

function findAll () {
    var xhr = new XMLHttpRequest();

    var body =
        {
            graph: new Array(response.edges.length),
            starting: document.getElementById('starting').value,
            final: document.getElementById('final').value
        };
    for (var i = 0; i < body.graph.length; i++) {
        body.graph[i] = {
            name: response.edges[i].id,
            start: {
                key: response.edges[i].source
            },
            finish: {
                key: response.edges[i].target
            },
            weight: response.edges[i].weight
        };
    }
    xhr.open("POST", "http://localhost:52446/api/pathes/all/");
    xhr.setRequestHeader('Content-Type', 'application/json');

    xhr.onreadystatechange = function () {
        if (request.readyState === 4 && request.status === 200) {
            all = JSON.parse(xhr.responseText);
            count = 0;
            document.getElementById('next').className = "";
            showNext();
        }
    };

    xhr.send(JSON.stringify(body));
}

function showNext() {
    if (!all || all.length === count) {
        document.getElementById('next').className = "hide";
        return;
    }
    cy.edges().animate({
        style: { lineColor: '#ddd' }
    });
    for (var i = 0; i < all[count].length; i++) {
        var param = "[id = " + "'" + all[count][i].name + "'" + "]";
        var ani = function (p) {
            return function ()
            { animate(p); };
        };
        setTimeout(ani(param), i*1000);
    }
    count++;
}

function animate(str) {
    cy.edges(str).animate({
        style: { lineColor: 'blue' }
    });
}
