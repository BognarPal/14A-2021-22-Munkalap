export class FailureModel {
    public id = 0;
    public issuer = '';
    public issueTimeStamp: Date = new Date();
    public room = '';
    public description = '';
    public assignedId: number | null  = null;
    public assignedName = '';
    public assignTimeStamp: Date | null = null;
    public assignComment = '';
    public workStarted: Date | null = null;
    public workFinished: Date | null = null;
    public finishComment = '';
    public isChecked: boolean | null = null;

    constructor(model: any = undefined) {
        if (model) {
            this.id = Number(model.id);
            this.issuer = model.issuer;
            this.issueTimeStamp = new Date(model.issueTimeStamp);
            this.room = model.room;
            this.description = model.description;
            if (model.assignedId) {
                this.assignedId = Number(model.assignedId);
                this.assignedName = model.assigned.name;
                this.assignTimeStamp = new Date(model.assignTimeStamp);
                this.assignComment = model.assignComment;
            }
            if (model.workStarted) {
                this.workStarted = new Date(model.workStarted);
            }
            if (model.workFinished) {
                this.workFinished = new Date(model.workFinished);
            }
            this.finishComment = model.finishComment;
            this.isChecked = model.isChecked;
        }
    }

    public get assignTimeStampStr() {
        return this.assignTimeStamp?.toISOString().replace('T', ' ').replace(/-/g, '.').substring(0, 16);
    }

    public get workStartedStr() {
        return this.workStarted?.toISOString().replace('T', ' ').replace(/-/g, '.').substring(0, 16);
    }

    public get workFinishedStr() {
        return this.workFinished?.toISOString().replace('T', ' ').replace(/-/g, '.').substring(0, 16);
    }
}