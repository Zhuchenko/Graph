var cy = null;
var graph = null;

(function() {
    let url = "http://localhost:52446/api/pathes/";

    let request = new XMLHttpRequest();
    request.open("GET", url);
    request.onreadystatechange = function () {
        if (request.readyState === 4 && request.status === 200) {
            graph = JSON.parse(request.responseText);
            draw();
        }
    };

    request.send();
}) ();

function draw() {
    let _nodes = new Array(graph.vertexes.length);

    for (let i = 0, l = _nodes.length; i < l; i++) {
        _nodes[i] = {
            data: { id: graph.vertexes[i] }
        };
    }

    let _edges = new Array(graph.edges.length);

    for (let i = 0, l = _edges.length; i < l; i++) {
        _edges[i] = {
            data: {
                id: graph.edges[i].id,
                source: graph.edges[i].source,
                target: graph.edges[i].target,
                weight: graph.edges[i].weight
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
            })
            .selector('.highlighted')
            .css({
                'background-color': '#61bffc',
                'line-color': '#61bffc',
                'target-arrow-color': '#61bffc',
                'transition-property': 'background-color, line-color, target-arrow-color',
                'transition-duration': '0.5s'
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

function findBest() {
    let url = "http://localhost:52446/api/pathes/best/";
    postRequest(url, clearAndAnimate);
}

function findAll() {
    let url = "http://localhost:52446/api/pathes/all/";
    postRequest(url, showListOfAll)
}

function postRequest(url, doAfter)
{
    let request = new XMLHttpRequest();

    request.open("POST", url);
    request.setRequestHeader('Content-Type', 'application/json');

    request.onreadystatechange = function () {
        if (request.readyState === 4 && request.status === 200) {
            response = JSON.parse(request.responseText);
            doAfter(response);
        }
    };

    let body = getBody();

    request.send(JSON.stringify(body));
}

function getBody() {
    let body =
        {
            graph: new Array(graph.edges.length),
            starting: document.getElementById('starting').value,
            final: document.getElementById('final').value
        };

    for (let i = 0, l = body.graph.length; i < l; i++) {
        body.graph[i] = {
            name: graph.edges[i].id,
            start: {
                key: graph.edges[i].source
            },
            finish: {
                key: graph.edges[i].target
            },
            weight: graph.edges[i].weight
        };
    }

    return body;
}

function clearAndAnimate(response) {
    cleanUp();

    animatePathById(response[0]);
}

function showListOfAll(all) {
    cleanUp();

    let list = document.createElement('select');
    list.id = 'select';
    list.size = 10;
    list.style.cssText = "position:fixed; color: black; left: 10px; top: 250px";
    list.onchange = function () {
        showPathByClick(all[this.selectedIndex]);
    }

    for (let i = 0, l = all.length; i < l; i++) {
        let text = all[i][0].start.key;
        for (let j = 0; j < all[i].length; j++) {
            text += '-' + all[i][j].finish.key;
        }
        let item = document.createElement('option');
        item.innerText = text;
        list.appendChild(item);
    }

    document.body.appendChild(list);
}

function showPathByClick(path) {
    clearGraph();

    animatePathById(path);
}

function animatePathById(path) {
    for (let i = 0, l = path.length; i < l; i++) {
        let element = cy.getElementById(path[i].name);

        setTimeout(function (e) {
            animate(e);
        }, i * 1000, element);
    }
}

function animatePathByIndex(path) {
    for (let i = 0, l = path.length; i < l; i++) {

        setTimeout(function (e) {
            animate(e);
        }, i * 1000, path[i]);

    }
}

function animate(element) {
    element.addClass('highlighted');
}

function standartBfs() {
    cleanUp();

    let root = '#' + document.getElementById('starting').value;
    let bfs = cy.elements().bfs(root, function () { }, true);

    animatePathByIndex(bfs.path);
}

function standartDfs() {
    cleanUp();

    let root = '#' + document.getElementById('starting').value;
    let dfs = cy.elements().dfs(root, function () { }, true);

    animatePathByIndex(dfs.path);
}

function standartDijkstra() {
    cleanUp();

    let root = '#' + document.getElementById('starting').value;

    let _dijkstra = cy.elements().dijkstra(root, function () {
        return this.data('weight');
    }, true);

    let goal = '#' + document.getElementById('final').value;
    let pathToFinal = _dijkstra.pathTo(cy.$(goal));

    animatePathByIndex(pathToFinal);
}

function standartStar() {
    cleanUp();

    let _root = '#' + document.getElementById('starting').value;
    let _goal = '#' + document.getElementById('final').value;

    let aStar = cy.elements().aStar({ root: _root, goal: _goal });

    animatePathByIndex(aStar.path);
}

function clearGraph() {
    for (let i = 0, l = cy.elements().length; i < l; i++) {
        cy.elements()[i].removeClass('highlighted');
    }
}

function clearList() {
    let child = document.getElementById('select');
    if (child)
        document.body.removeChild(child);
}

function cleanUp() {
    clearGraph();
    clearList();
}