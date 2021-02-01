import React, { Component } from 'react';
import Modal from "react-bootstrap/Modal";
import DatasetInput from './DatasetInput';
import axios from 'axios';
import { ForceGraph2D } from 'react-force-graph';
import "bootstrap/dist/css/bootstrap.min.css";

export class Datasets extends Component {
    static displayName = Datasets.name;

  constructor(props) {
    super(props);
      this.state = {
          datasets: [],
          loading: true,
          openMessage: false,
          openStatistics: false,
          openGraph: false,
          message: '',
          datasetStatistics: {},
          datasetGraph: {
              nodes: [],
              links: []
          },
          graphConfig: {
              nodeHighlightBehavior: true,
              focusAnimationDuration: 0,
              width: 800,
              height: 800,
              maxZoom: 12,
              node: {
                  color: 'lightblue',
                  size: 120,
                  highlightStrokeColor: 'blue',
                  renderLabel: false
              },
              link: {
                  highlightColor: 'lightblue'
              }
          }
      };
  }

  componentDidMount() {
      this.populateDatasets();
  }


  datasetAPI(url = '/Datasets'){
        return {
            create: newRecord => axios.post(url, newRecord),
            update: (id, updateRecord) => axios.put(url + '/' + id, updateRecord),
            delete: id => axios.delete(url + '/' + id)
        }
    }

    graphAPI(url = '/Graph') {
        return {
            getNodes: id => axios.get(url + '/Nodes/' + id),
            getLinks: id => axios.get(url + '/Links/' + id)
        }
    }

    getDeleteDataset(datasetId) {
        const f = () => {
            this.datasetAPI().delete(datasetId).then(res => {
                this.populateDatasets()
            }).catch(error => {
                console.log(error);
                if (error.response && error.response.data && error.response.data.errorMessage) {
                    this.message = error.response.data.errorMessage;
                    this.openMessage = true;
                }
            }
            )
        }
        f.bind(this);
        return f;
    }

    getOpenStatistics(id, name, usersCount, linksCount, avgFriendsCount) {
        const f = () => {
            this.setState({
                datasetStatistics: {
                    id: id,
                    name: name,
                    usersCount: usersCount,
                    linksCount: linksCount,
                    avgFriendsCount: avgFriendsCount
                },
                openStatistics: true
            })
        };
        f.bind(this);
        return f
    }

    getCloseStatistics() {
        const f = () => {
            this.setState({
                datasetStatistics: {},
                openStatistics: false
            })
        };
        f.bind(this);
        return f
    }

    getOpenGraph(id, name) {
        const f = () => {
            this.graphAPI().getNodes(id).then(nodes => {
                this.graphAPI().getLinks(id).then(links => {                    
                    this.setState({
                        datasetGraph: {
                            name: name,
                            nodes: nodes.data,
                            links: links.data,
                        },
                        openGraph: true
                    })
                }).catch (error => {
                    console.log(error);
                    if (error.response && error.response.data && error.response.data.errorMessage) {
                        this.message = error.response.data.errorMessage;
                        this.openMessage = true;
                    }
                }
                );
            }).catch(error => {
                console.log(error);
                if (error.response && error.response.data && error.response.data.errorMessage) {
                    this.message = error.response.data.errorMessage;
                    this.openMessage = true;
                }
            }
            );
        };
        f.bind(this);
        return f
    }

    getCloseGraph() {
        const f = () => {
            this.setState({
                openGraph: false
            })
        };
        f.bind(this);
        return f
    }

    openMessage(msg) {
        this.setState({
            message: msg,
            openMessage: true
        })
    }

    closeMessage() {
        this.setState({
            openMessage: false
        })
    }

    async populateDatasets() {
        const response = await fetch('Datasets');
        const data = await response.json();
        this.setState({ datasets: data, loading: false });
    }

    renderDatasetsTable(datasets) {

        return (       
          <table className='table table-striped table-hover caption-top' aria-labelledby="tabelLabel">
              <thead>
                  <tr>
                        <th className="w-25 p-3">Name</th>
                        <th className="w-50 p-3">Description</th>
                        <th/>
                  </tr>
              </thead>
              <tbody>
                    {datasets.map(dataset =>
                        <tr key={dataset.id}>
                            <td>{dataset.name}</td>
                            <td>{dataset.description}</td>
                            <td style={{textAlign: "right"}}>
                                <a role="button" title="Visualization" onClick={this.getOpenGraph(dataset.id, dataset.name)}>

                                    <svg xmlns="http://www.w3.org/2000/svg" width="32" height="32" fill="#007bff" className="bi bi-diagram-2" viewBox="0 0 20 22">
                                        <path fillRule="evenodd" d="M6 3.5A1.5 1.5 0 0 1 7.5 2h1A1.5 1.5 0 0 1 10 3.5v1A1.5 1.5 0 0 1 8.5 6v1H11a.5.5 0 0 1 .5.5v1a.5.5 0 0 1-1 0V8h-5v.5a.5.5 0 0 1-1 0v-1A.5.5 0 0 1 5 7h2.5V6A1.5 1.5 0 0 1 6 4.5v-1zM8.5 5a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1zM3 11.5A1.5 1.5 0 0 1 4.5 10h1A1.5 1.5 0 0 1 7 11.5v1A1.5 1.5 0 0 1 5.5 14h-1A1.5 1.5 0 0 1 3 12.5v-1zm1.5-.5a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1zm4.5.5a1.5 1.5 0 0 1 1.5-1.5h1a1.5 1.5 0 0 1 1.5 1.5v1a1.5 1.5 0 0 1-1.5 1.5h-1A1.5 1.5 0 0 1 9 12.5v-1zm1.5-.5a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1z" />
                                    </svg>
                                </a>
                                <a role="button" title="Statistics" onClick={this.getOpenStatistics(dataset.id, dataset.name, dataset.usersCount, dataset.linksCount, dataset.avgFriendsCount)}>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" fill="#007bff" className="bi bi-card-text" viewBox="0 0 22 22">
                                        <path d="M14.5 3a.5.5 0 0 1 .5.5v9a.5.5 0 0 1-.5.5h-13a.5.5 0 0 1-.5-.5v-9a.5.5 0 0 1 .5-.5h13zm-13-1A1.5 1.5 0 0 0 0 3.5v9A1.5 1.5 0 0 0 1.5 14h13a1.5 1.5 0 0 0 1.5-1.5v-9A1.5 1.5 0 0 0 14.5 2h-13z" />
                                        <path d="M3 5.5a.5.5 0 0 1 .5-.5h9a.5.5 0 0 1 0 1h-9a.5.5 0 0 1-.5-.5zM3 8a.5.5 0 0 1 .5-.5h9a.5.5 0 0 1 0 1h-9A.5.5 0 0 1 3 8zm0 2.5a.5.5 0 0 1 .5-.5h6a.5.5 0 0 1 0 1h-6a.5.5 0 0 1-.5-.5z" />
                                    </svg>
                                </a>
                                <a role="button" title="Remove" onClick={this.getDeleteDataset(dataset.id)}>
                                    <svg xmlns="http://www.w3.org/2000/svg" width="30" height="30" fill="#007bff" className="bi bi-trash" viewBox="0 0 22 22">
                                        <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z" />
                                        <path fillRule="evenodd" d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4L4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z" />
                                    </svg>
                                </a>
                            </td>
                        </tr>
                  )}
              </tbody>
          </table>
    );
  }


  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
        : this.renderDatasetsTable(this.state.datasets);

    return (
      <div className="row">
            <div>
                <DatasetInput onUpload={this.populateDatasets.bind(this)} openMessage={this.openMessage.bind(this)} />
            </div>
            <div>
                <div className="container text-center">
                    <h1>Existing Datasets</h1>
                </div> 
                <div className="container" id='datasetStatistics'>
                    <Modal show={this.state.openStatistics} onHide={this.getCloseStatistics()}>
                        <Modal.Header closeButton>
                            <Modal.Title>Statistics for dataset {this.state.datasetStatistics.name}</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <div className="container">
                                <div className="row">
                                    <div className="col-sm">Total links count:</div><div className="col-sm">{this.state.datasetStatistics.linksCount}</div>                                    
                                </div>
                                <div className="row">
                                    <div className="col-sm">Total users count:</div><div className="col-sm">{this.state.datasetStatistics.usersCount}</div>
                                </div>
                                <div className="row">
                                    <div className="col-sm">Avg. friends per person:</div><div className="col-sm">{this.state.datasetStatistics.avgFriendsCount ? this.state.datasetStatistics.avgFriendsCount.toFixed(2) : null}</div>
                                </div>
                            </div>                        
                            </Modal.Body>
                        <Modal.Footer/>
                    </Modal>
                    <Modal show={this.state.openGraph} onHide={this.getCloseGraph()} size="xl">
                        <Modal.Header closeButton>
                            <Modal.Title>Visualization of dataset {this.state.datasetGraph.name}</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <div>
                                <ForceGraph2D width={1100}
                                graphData={this.state.datasetGraph}
                                />
                            </div>
                                

                        </Modal.Body>
                    </Modal>
                    <Modal show={this.state.openMessage} onHide={this.closeMessage.bind(this)} size="lg">
                        <Modal.Header closeButton>
                            <Modal.Title>Error!</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <div id="error" className="alert alert-danger" role="alert">{this.state.message}</div>
                        </Modal.Body>
                    </Modal> 
                </div> 
                {contents}
             </div>
            
      </div>
    );
    }
}
