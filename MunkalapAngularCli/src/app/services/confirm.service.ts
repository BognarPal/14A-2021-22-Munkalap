import { ApplicationRef, ComponentFactoryResolver, EmbeddedViewRef, Injectable, Injector } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ConfirmComponent } from '../components/confirm/confirm.component';

@Injectable({
  providedIn: 'root'
})
export class ConfirmService {

  constructor(
    private factoryResolver: ComponentFactoryResolver,
    private appRef: ApplicationRef,
    private injector: Injector
  ) { }

  public confirm(confirmHTML: string): Observable<boolean> {
    const factory = this.factoryResolver.resolveComponentFactory(ConfirmComponent);
    const componentRef = factory.create(this.injector);
    this.appRef.attachView(componentRef.hostView);
    const domElem = (componentRef.hostView as EmbeddedViewRef<any>).rootNodes[0] as HTMLElement;
    document.body.appendChild(domElem);

    componentRef.instance.confirmHTML = confirmHTML;
    return componentRef.instance.afterClosed.pipe(
      map( result => {
        componentRef.destroy();
        if (result === null) {
          return false;
        }
        return result;
      })); 
  }
}
