 
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
const HttpUploadOptions = {
  headers: new HttpHeaders({ "Content-Type": "multipart/form-data" })
}

@Injectable({
  providedIn: 'root'
})
export class DistritosService {

  URL = environment.URL_API;
  constructor(private http:HttpClient) { }

  get_mostrarDistrito_general(){
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '1');
    parametros = parametros.append('filtro',  '' );

    return this.http.get( this.URL + 'tblDistritos' , {params: parametros});
  }
  
  set_saveDistrito(objDistrito:any){
    return this.http.post(this.URL + 'tblDistritos', JSON.stringify(objDistrito), httpOptions);
  }

  set_editDistrito(objDistrito:any, idDistrito :number){
    return this.http.put(this.URL + 'tblDistritos/' + idDistrito , JSON.stringify(objDistrito), httpOptions);
  }

  set_anularDistrito(idDistrito : number){ 
    let parametros = new HttpParams();
    parametros = parametros.append('opcion', '2');
    parametros = parametros.append('filtro',  String(idDistrito));

    return this.http.get( this.URL + 'tblDistritos' , {params: parametros});
  }
  
}
